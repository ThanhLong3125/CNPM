using System.Security.Claims;
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("diagnosis")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Tạo chẩn đoán mới (có thể đính kèm ảnh)")]
        public async Task<ActionResult<object>> CreateDiagnosisWithImage([FromForm] CreateDiagnosisWithOptionalImageDto dto)
        {
            try
            {
                var diagnosisId = await _doctorService.CreateDiagnosisWithOptionalImageAsync(dto);
                return Ok(new { message = "Tạo chẩn đoán thành công", diagnosisId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Doctor/doctor
        [HttpPost("doctor")]
        [SwaggerOperation(Summary = "Tạo chẩn đoán")]
        public async Task<IActionResult> CreateDiagnosis([FromBody] CreateDiagnosisDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var diagnosisId = await _doctorService.CreateDiagnosisAsync(dto);
                return Ok(new { message = "Tạo chẩn đoán thành công!", diagnosisId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return BadRequest(
                    new { error = ex.Message, innerError = ex.InnerException?.Message }
                );
            }
        }

        // PUT: api/Doctor/doctor/{diagnosisId}
        [HttpPut("doctor/{diagnosisId}")]
        [SwaggerOperation(Summary = "Chỉnh sửa chẩn đoán")]
        public async Task<IActionResult> UpdateDiagnosis([FromRoute] string diagnosisId, [FromBody] UpdateDiagnosisDto dto)
        {
            try
            {
                var updatedDiagnosis = await _doctorService.UpdateDiagnosisAsync(diagnosisId, dto);
                return Ok(updatedDiagnosis);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/Doctor/doctor/{medicalRecordId}
        [HttpGet("doctor/medicalRecord/{medicalRecordId}")]
        [SwaggerOperation(Summary = "Tìm kiếm Diagnosis theo MedicalRecordId")]
        public async Task<IActionResult> GetDiagnosisbyMedicalRecordId([FromRoute] string medicalRecordId)
        {
            try
            {
                var diagnoses = await _doctorService.SearchDiagnosisbyMRAsync(medicalRecordId);
                return Ok(diagnoses);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("waiting-patients")]
        [SwaggerOperation(Summary = "Lấy danh sách bệnh nhân đang chờ khám")]
        public async Task<IActionResult> GetWaitingPatients()

        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var records = await _doctorService.ListWaitingPatientAsync(userId);
            return Ok(records);
        }

        [HttpGet("treated-patients")]
        [SwaggerOperation(Summary = "Lấy danh sách bệnh nhân đã được khám")]
        public async Task<IActionResult> GetTreatedPatients()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }
            var records = await _doctorService.ListTreatedPatientAsync(userId);
            return Ok(records);
        }
    }
}
