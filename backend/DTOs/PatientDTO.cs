public class PatientDto
{
    public int Id { get; set; }            // Mã bệnh nhân
    public string FullName { get; set; } = string.Empty;  // Họ và tên
    public DateTime DateOfBirth { get; set; }             // Ngày sinh
    public string Gender { get; set; } = string.Empty;    // Giới tính
    public string Email { get; set; } = string.Empty;     // Email hoặc thông tin liên lạc
}
