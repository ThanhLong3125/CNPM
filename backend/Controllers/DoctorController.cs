using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HanniApi.Controllers
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

        // POST: api/StaffReception/patient
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
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("doctor")]
        [SwaggerOperation(Summary = "Chỉnh sửa chẩn đoán")]
        public async Task<IActionResult> UpdateDiagnosis(Guid id, [FromBody] UpdateDiagnosisDto dto)
        {
            try
            {
                var updatedDiagnosis = await _doctorService.UpdateDiagnosisAsync(id, dto);
                return Ok(updatedDiagnosis);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("doctor/{id}")]
        [SwaggerOperation(Summary = "Tìm kiếm Diagnosis theo MedicalRecordId")]
        public async Task<IActionResult> GetDiagnosisbyId([FromRoute] Guid id)
        {
            try
            {
                var diagnosis = await _doctorService.SearchDiagnosisbyMRAsync(id);
                return Ok(diagnosis);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Lỗi máy chủ. Vui lòng thử lại sau." });
            }
        }
    }
}
