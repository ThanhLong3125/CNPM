// Services/FileStorageService.cs

using backend.Configurations;
using Microsoft.Extensions.Options;

namespace backend.Services
{
    // Interfaces/IFileStorageService.cs

    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);

        string GetFilePath(string fileName);

        bool FileExists(string fileName);

        void DeleteFile(string fileName);

        Task<byte[]> ReadFileBytesAsync(string fileName);
    }

    // Services/LocalFileStorageService.cs (Implementation for local file system)

    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _baseUploadDirectory;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalFileStorageService(
            IWebHostEnvironment webHostEnvironment,
            IOptions<FileStorageSettings> fileStorageOptions
        )
        {
            _webHostEnvironment = webHostEnvironment;

            // This combines WebRootPath with the configured uploads folder for a publicly accessible path

            _baseUploadDirectory = fileStorageOptions.Value.UploadsFolder;

            // Ensure the directory exists

            if (!Directory.Exists(_baseUploadDirectory))
            {
                Directory.CreateDirectory(_baseUploadDirectory);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);

            string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

            string directoryPath = _baseUploadDirectory;

            string filePath = Path.Combine(directoryPath, uniqueFileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName; // Return only the unique file name
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_baseUploadDirectory, fileName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }

        public void DeleteFile(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<byte[]> ReadFileBytesAsync(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found at {filePath}");
            }

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}
