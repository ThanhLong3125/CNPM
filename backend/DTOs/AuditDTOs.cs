namespace backend.DTOs
{
    public class WriteLogDto
    {
        public string User { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public object Details { get; set; } = string.Empty;
    }
}