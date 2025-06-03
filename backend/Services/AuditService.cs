using backend.DTOs;

namespace backend.Services
{
    public interface IAuditService
    {
        Task WriteLogAsync(WriteLogDto dto);
        Task<string> GetLogAsync();
    }

    public class AuditService : IAuditService
    {
        private readonly string _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        private readonly string _logFileName = "audit.log";

        public async Task WriteLogAsync(WriteLogDto dto)
        {
            // Đảm bảo thư mục Logs tồn tại
            if (!Directory.Exists(_logFolderPath))
            {
                Directory.CreateDirectory(_logFolderPath);
            }

            var fullPath = Path.Combine(_logFolderPath, _logFileName);

            var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | User: {dto.User} | Action: {dto.Action} | Details: {System.Text.Json.JsonSerializer.Serialize(dto.Details)}";

            await File.AppendAllTextAsync(fullPath, logEntry + Environment.NewLine);
        }

        public async Task<string> GetLogAsync()
        {
            var fullPath = Path.Combine(_logFolderPath, _logFileName);

            if (!File.Exists(fullPath))
                return "Log file not found.";
            return await File.ReadAllTextAsync(fullPath);
        }
    }
}
