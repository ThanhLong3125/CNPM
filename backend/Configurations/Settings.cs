namespace backend.Configurations
{
    public class FileStorageSettings
    {
        public string UploadsFolder { get; set; } = null!;
    }

    public class GeminiApiSettings
    {
        public string BaseUrl { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
    }
}
