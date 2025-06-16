// Services/AIService.cs
using System.Net;
using System.Text.Json;
using backend.Configurations;
using Microsoft.Extensions.Options;

namespace backend.Services
{
    // ---
    // Interface for the AI Analysis Service (specifically for Gemini)
    public interface IAIAnalysisService
    {
        // Now returns just the analysis string, as its job is to analyze, not update DB or return DTO.
        // It takes raw image bytes and mime type, abstracting file storage from this service.
        Task<string> AnalyzeImageWithGeminiAsync(
            byte[] imageBytes,
            string prompt,
            string mimeType = "image/jpeg"
        );
    }

    // ---
    // Implementation of the AI Analysis Service using Gemini API
    public class GeminiAnalysisService : IAIAnalysisService
    {
        private readonly HttpClient _httpClient; // Injected Typed HttpClient, pre-configured
        private readonly string _geminiApiKey;
        private readonly string _geminiBaseUrl; // This will now typically include the model path, or BaseAddress will

        // Constructor for the Gemini Analysis Service
        public GeminiAnalysisService(
            HttpClient httpClient, // Inject HttpClient (expected to be a Typed Client configured in Program.cs)
            IOptions<GeminiApiSettings> geminiApiOptions // Inject IOptions for GeminiApi settings
        )
        {
            _httpClient = httpClient;

            // Get Gemini API settings from configuration
            _geminiBaseUrl =
                geminiApiOptions.Value.BaseUrl
                ?? throw new InvalidOperationException(
                    "GeminiApi:BaseUrl configuration is missing. Please ensure it's set in appsettings.json."
                );

            _geminiApiKey =
                geminiApiOptions.Value.ApiKey
                ?? throw new InvalidOperationException(
                    "GeminiApi:ApiKey configuration is missing. Please ensure it's set in appsettings.json."
                );

            // NOTE: If you are setting the BaseAddress for this HttpClient via AddHttpClient<GeminiAnalysisService>(client => ...)
            // in Program.cs, then _geminiBaseUrl should be just the domain or the base model path,
            // and you'd only append the API key as a query parameter.
            // Example: If client.BaseAddress is "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent"
            // then _geminiBaseUrl might not be needed explicitly here, or it should be just the key part.
            // For now, I'm assuming _geminiBaseUrl is the *full endpoint path* for simplicity, as per your original code.
            // If it's just the base, you'd combine it like: $"{_geminiBaseUrl}/v1beta/models/gemini-2.0-flash:generateContent?key={_geminiApiKey}"
            // For robust typed HttpClient, the full BaseAddress (including model path) is ideal.
        }

        // ---
        // Method to analyze an image using the Gemini API
        public async Task<string> AnalyzeImageWithGeminiAsync(
            byte[] imageBytes,
            string prompt,
            string mimeType = "image/jpeg"
        )
        {
            // Convert image bytes to Base64 string for the API payload
            string base64Image = Convert.ToBase64String(imageBytes);

            // Construct the Gemini API request payload
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new { text = prompt },
                            new { inlineData = new { mimeType = mimeType, data = base64Image } },
                        },
                    },
                },
            };

            // Construct the full request URI including the API key
            // Assuming _geminiBaseUrl already contains the model endpoint (e.g., ".../gemini-2.0-flash:generateContent")
            var requestUri = $"{_geminiBaseUrl}?key={_geminiApiKey}";

            try
            {
                // Serialize the request body to JSON
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    System.Text.Encoding.UTF8,
                    "application/json" // Explicitly set Content-Type header for the request body
                );

                // Send the POST request to the Gemini API
                var response = await _httpClient.PostAsync(requestUri, jsonContent);

                // Check for successful HTTP status code (2xx)
                response.EnsureSuccessStatusCode();

                // Read the JSON response string
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to extract the AI analysis text
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    if (
                        root.TryGetProperty("candidates", out JsonElement candidates)
                        && candidates.GetArrayLength() > 0
                        && candidates[0].TryGetProperty("content", out JsonElement content)
                        && content.TryGetProperty("parts", out JsonElement parts)
                        && parts.GetArrayLength() > 0
                        && parts[0].TryGetProperty("text", out JsonElement textPart)
                    )
                    {
                        return textPart.GetString() ?? "AI analysis text is null.";
                    }
                    else
                    {
                        // Log the full unexpected response for debugging purposes
                        Console.WriteLine(
                            $"Gemini API unexpected response structure: {jsonResponse}"
                        );
                        throw new ApplicationException(
                            "Gemini API response did not contain expected analysis text. Check API response structure."
                        );
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP request error to Gemini API: {httpEx.Message}");

                string userMessage =
                    "An error occurred while connecting to the AI service. Please check network connection or API configuration.";
                if (httpEx.StatusCode.HasValue)
                {
                    userMessage = httpEx.StatusCode switch
                    {
                        HttpStatusCode.NotFound => "AI service endpoint not found.",
                        HttpStatusCode.Unauthorized =>
                            "AI service authorization failed. Check your API key.",
                        HttpStatusCode.Forbidden => "Access to AI service forbidden.",
                        HttpStatusCode.BadRequest => "Invalid request sent to AI service.",
                        _ =>
                            $"AI service error: Status code {httpEx.StatusCode}. Please try again.",
                    };
                }
                throw new ApplicationException(userMessage, httpEx);
            }
            catch (JsonException jsonEx)
            {
                // Error parsing JSON response
                Console.WriteLine($"JSON deserialization error from Gemini API: {jsonEx.Message}");
                throw new ApplicationException($"Lỗi xử lý phản hồi từ dịch vụ AI.", jsonEx);
            }
            catch (Exception ex)
            {
                // Any other unexpected errors
                Console.WriteLine(
                    $"An unexpected error occurred during Gemini analysis: {ex.Message}"
                );
                Console.ReadLine();
                throw new ApplicationException(
                    $"Đã xảy ra lỗi không mong muốn khi phân tích ảnh bằng AI. Vui lòng thử lại sau.",
                    ex
                );
            }
        }
    }
}
