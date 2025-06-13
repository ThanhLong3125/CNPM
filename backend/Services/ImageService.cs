// Services/ImageService.cs
using backend.Data;
using backend.DTOs;
using backend.Models;

namespace backend.Services
{
    public interface IImageService
    {
        Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto);
    }

    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context; // Your DbContext

        public ImageService(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto)
        {
            // Define the directory where images will be saved
            // For local development, you might use a path like:
            // string uploadDir = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Images");
            // Or a specific local directory:
            string uploadDir = "/home/pampam/Documents/UTH/phan_mem/CNPM/backend/images";

            // Ensure the directory exists
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Generate a unique filename to prevent overwriting existing files
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadImageDto.File.FileName;
            string filePath = Path.Combine(uploadDir, uniqueFileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadImageDto.File.CopyToAsync(stream);
            }

            // Create a new Image model instance
            var image = new Image
            {
                Id = Guid.NewGuid(), // Generate a new GUID for the image
                Path = filePath, // Store the full path in the database
                DiagnosisId = uploadImageDto.DiagnosisId,
                UploadDate = DateTime.UtcNow,
                AIAnalysis = null, // Or set a default/empty string
            };

            // Add the image to the database
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            // Map to ImageDto for the response
            var imageDto = new ImageDto
            {
                Id = image.Id,
                UploadDate = image.UploadDate,
                AIAnalysis = image.AIAnalysis ?? string.Empty,
                DiagnosisId = image.DiagnosisId,
                Path = image.Path,
            };

            return imageDto;
        }
    }
}
