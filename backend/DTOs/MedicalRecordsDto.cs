public class MedicalRecordsDto
{
    public int RecordId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string Symptoms { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
