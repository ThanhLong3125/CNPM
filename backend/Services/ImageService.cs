// Services/ImageService.cs
using backend.Configurations;
using backend.Data;
using backend.DTOs;
using backend.Exceptions;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options; // Still needed if you use IOptions within this service, but not for direct config access here anymore.

namespace backend.Services
{
    // ---
    // Interface for Image-related operations
    public interface IImageService
    {
        Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto);
        Task<ImageDto> GetImageByIdAsync(Guid imageId);
        Task DeleteImageAsync(Guid imageId);
        Task<ImageDto> AnalyzeImageAsync(Guid imageId); // Method to trigger AI analysis
    }

    // ---
    // Implementation of ImageService
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;
        private readonly IAIAnalysisService _aiAnalysisService; // Injected AI service
        private readonly IFileStorageService _fileStorageService; // Injected file storage service
        private readonly string _uploadsFolderUrlPath; // To construct public URLs for images

        // Constructor: Inject only the services this class directly depends on
        public ImageService(
            AppDbContext context,
            IAIAnalysisService aiAnalysisService, // Now injecting the AI analysis service
            IFileStorageService fileStorageService, // Still injecting the file storage service
            IOptions<FileStorageSettings> fileStorageOptions // To get the public URL path for images
        )
        {
            _context = context;
            _aiAnalysisService = aiAnalysisService;
            _fileStorageService = fileStorageService;

            // Get the configured uploads folder name for constructing public URLs
            _uploadsFolderUrlPath = "/" + (fileStorageOptions.Value.UploadsFolder ?? "images");
            // Example: If UploadsFolder is "images", this becomes "/images"
            // This is used for creating URLs like "/images/your_file.jpg"
        }

        // ---
        // Helper method to retrieve an Image entity by ID
        private async Task<Image> GetImageEntityByIdAsync(Guid imageId)
        {
            var image = await _context
                .Images.Include(i => i.Diagnosis) // Include Diagnosis for context, if needed later
                .FirstOrDefaultAsync(i => i.Id == imageId && !i.IsDeleted);

            if (image == null)
            {
                throw new NotFoundException($"Image with ID {imageId} not found.");
            }
            return image;
        }

        // ---
        // Uploads a new image file and creates an Image record in the database
        public async Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto)
        {
            // Validate DiagnosisId exists and is not deleted
            var diagnosis = await _context.Diagnoses.FirstOrDefaultAsync(d =>
                d.Id == uploadImageDto.DiagnosisId && !d.IsDeleted
            );
            if (diagnosis == null)
            {
                throw new NotFoundException(
                    $"Diagnosis with ID {uploadImageDto.DiagnosisId} not found or is deleted."
                );
            }

            // Delegate file saving to the FileStorageService
            // It returns only the unique file name (e.g., "b23e3f4a-5b6c-4d7e-8f90-1a2b3c4d5e6f.jpg")
            string uniqueFileName = await _fileStorageService.SaveFileAsync(uploadImageDto.File);

            var image = new Image
            {
                Id = Guid.NewGuid(),
                Path = uniqueFileName, // Store only the unique file name in the database
                DiagnosisId = uploadImageDto.DiagnosisId,
                UploadDate = DateTime.UtcNow,
                AIAnalysis = null, // Initial state, analysis happens later
                ImageName = uploadImageDto.ImageName, // Save the provided image name
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            // Convert to DTO for returning, including the public URL path
            return image.ToDto(_uploadsFolderUrlPath);
        }

        // ---
        // Retrieves image details by ID
        public async Task<ImageDto> GetImageByIdAsync(Guid imageId)
        {
            var image = await GetImageEntityByIdAsync(imageId); // Use helper

            // Convert to DTO for returning, including the public URL path
            return image.ToDto(_uploadsFolderUrlPath);
        }

        // ---
        // Soft deletes an image record by ID
        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await GetImageEntityByIdAsync(imageId); // Use helper

            // Perform soft delete
            image.IsDeleted = true;
            _context.Images.Update(image); // Mark as modified
            await _context.SaveChangesAsync();

            // Optional: If you want to physically delete the file, add logic here:
            // try
            // {
            //     _fileStorageService.DeleteFile(image.Path);
            // }
            // catch (FileNotFoundException ex)
            // {
            //     // Log: file already gone, but DB record updated
            //     Console.WriteLine($"Warning: Physical file for {image.Path} not found during deletion: {ex.Message}");
            // }
            // catch (Exception ex)
            // {
            //     // Log other file deletion errors
            //     Console.WriteLine($"Error deleting physical file for {image.Path}: {ex.Message}");
            //     // Depending on policy, you might re-throw or just log
            // }
        }

        // ---
        // Triggers AI analysis for a specific image
        public async Task<ImageDto> AnalyzeImageAsync(Guid imageId)
        {
            var image = await GetImageEntityByIdAsync(imageId); // Get image from DB

            // Ensure the physical file exists before attempting AI analysis
            if (!_fileStorageService.FileExists(image.Path))
            {
                throw new NotFoundException(
                    $"Physical image file '{image.Path}' not found for image ID {imageId}. Cannot analyze."
                );
            }

            // Read image bytes using the file storage service
            byte[] imageBytes = await _fileStorageService.ReadFileBytesAsync(image.Path);

            // Determine MIME type (you might need a helper to guess from extension or store it in DB)
            string mimeType = GetMimeTypeFromFileName(image.Path); // Example: "image/jpeg"

            // Construct the prompt for the AI
            string prompt = "What is this image?"; // Default prompt

            // Optionally, enhance the prompt with diagnosis context if available
            if (image.Diagnosis != null)
            {
                // You can make this prompt more sophisticated based on diagnosis notes
                prompt =
                    $"Analyze this image related to diagnosis notes: '{image.Diagnosis.Notes}'. {prompt}";
            }

            // Delegate AI analysis to the AIAnalysisService
            string aiAnalysisResult = await _aiAnalysisService.AnalyzeImageWithGeminiAsync(
                imageBytes,
                prompt,
                mimeType
            );

            // Update the Image entity with the analysis result
            image.AIAnalysis = aiAnalysisResult;
            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            // Return the updated ImageDto, including the new AI analysis
            return image.ToDto(_uploadsFolderUrlPath);
        }

        // ---
        // Helper to determine MIME type from file extension (basic implementation)
        private string GetMimeTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream", // Default if unknown
            };
        }
    }

    // ---
    // Extension method for clean DTO mapping
    public static class ImageExtensions
    {
        public static ImageDto ToDto(this Image image, string uploadsFolderUrlPath)
        {
            return new ImageDto
            {
                Id = image.Id,
                ImageName = image.ImageName,
                UploadDate = image.UploadDate,
                AIAnalysis = image.AIAnalysis ?? string.Empty,
                DiagnosisId = image.DiagnosisId,
                // Construct the public URL path
                Path = $"{uploadsFolderUrlPath}/{image.Path}",
            };
        }
    }
}
