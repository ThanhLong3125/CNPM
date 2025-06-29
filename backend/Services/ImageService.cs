using backend.Configurations;
using backend.Data;
using backend.DTOs;
using backend.Exceptions;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

namespace backend.Services
{
    public interface IImageService
    {
        Task<Image> UploadImageAsync(UploadImageDto uploadImageDto);
        Task<Image> GetImageByIdAsync(string imageId);
        Task DeleteImageAsync(string imageId);
        Task<ImageDto> AnalyzeImageAsync(string imageId);
    }

    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;
        private readonly IAIAnalysisService _aiAnalysisService;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _uploadsFolderUrlPath;

        public ImageService(
            AppDbContext context,
            IAIAnalysisService aiAnalysisService,
            IFileStorageService fileStorageService,
            IOptions<FileStorageSettings> fileStorageOptions
        )
        {
            _context = context;
            _aiAnalysisService = aiAnalysisService;
            _fileStorageService = fileStorageService;
            _uploadsFolderUrlPath = "/" + (fileStorageOptions.Value.UploadsFolder ?? "images");
        }


        public async Task<Image> UploadImageAsync(UploadImageDto uploadImageDto)
        {
            var diagnosis = await _context.Diagnoses.FirstOrDefaultAsync(d =>
                d.DiagnosisId == uploadImageDto.DiagnosisId && !d.IsDeleted
            );

            if (diagnosis == null)
            {
                throw new NotFoundException($"Diagnosis ID '{uploadImageDto.DiagnosisId}' not found.");
            }

            string uniqueFileName = await _fileStorageService.SaveFileAsync(uploadImageDto.File);

            var image = new Image
            {
                Id = Guid.NewGuid(),
                ImageId = $"IM{Guid.NewGuid():N}".Substring(0, 6).ToUpper(),
                Path = uniqueFileName,
                DiagnosisId = uploadImageDto.DiagnosisId,
                UploadDate = DateTime.UtcNow,
                AIAnalysis = null,
                ImageName = uploadImageDto.ImageName,
                IsDeleted = false,
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<Image> GetImageByIdAsync(string imageId)
        {
            var image = await _context.Images
                .FirstOrDefaultAsync(i => i.ImageId == imageId);
            if (image == null) throw new Exception("Không tìm thấy hình ảnh.");
            return image;
        }


        public async Task DeleteImageAsync(string imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ImageId == imageId);
            if (image == null) throw new Exception("Không tìm thấy hình ảnh.");
            image.IsDeleted = true;
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ImageDto> AnalyzeImageAsync(string imageId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ImageId == imageId);
            if (image == null) throw new Exception("Không tìm thấy hình ảnh.");
            if (!_fileStorageService.FileExists(image.Path))
            {
                throw new NotFoundException(
                    $"File '{image.Path}' for image ID '{imageId}' not found."
                );
            }

            byte[] imageBytes = await _fileStorageService.ReadFileBytesAsync(image.Path);
            string mimeType = GetMimeTypeFromFileName(image.Path);
            string prompt = "Ảnh này bạn thấy gì trong nó(trả lời bằng tiếng việt)?";

            if (image.Diagnosis != null)
            {
                prompt = $"Phân tích chuẩn đoán bức ảnh: '{image.Diagnosis.Notes}'. {prompt}";
            }

            string aiAnalysisResult = await _aiAnalysisService.AnalyzeImageWithGeminiAsync(
                imageBytes,
                prompt,
                mimeType
            );

            image.AIAnalysis = aiAnalysisResult;
            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            return image.ToDto(_uploadsFolderUrlPath);
        }

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
                _ => "application/octet-stream",
            };
        }
    }

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
                Path = $"{uploadsFolderUrlPath}/{image.Path}",
            };
        }
    }
}
