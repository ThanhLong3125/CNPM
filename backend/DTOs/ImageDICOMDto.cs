public class ImageDICOMDto
{
    public int Id { get; set; }                      // Mã ảnh DICOM
    public string FilePath { get; set; } = string.Empty;  // Đường dẫn file ảnh
    public DateTime AcquisitionDateTime { get; set; }     // Thời gian chụp ảnh
    public string ImageType { get; set; } = string.Empty; // Loại ảnh (ví dụ: X-ray, MRI)
}
