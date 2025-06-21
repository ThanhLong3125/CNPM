using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffReceptionController : ControllerBase
    {
        private readonly IStaffReceptionService _staffReceptionService;

        public StaffReceptionController(IStaffReceptionService staffReceptionService)
        {
            _staffReceptionService = staffReceptionService;
        }

        // POST: api/StaffReception/patient
        [HttpPost("patient")]
        [SwaggerOperation(Summary = "Tạo hồ sơ bệnh nhân")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var patientId = await _staffReceptionService.CreatePatientAsync(dto);
                return Ok(new { message = "Tạo hồ sơ bệnh nhân thành công", patientId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("patients")]
        [SwaggerOperation(Summary = "Chỉnh sửa hồ sơ bệnh nhân")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientDto dto)
        {
            try
            {
                var updatedPatient = await _staffReceptionService.UpdatePatientAsync(id, dto);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        // GET: api/StaffReception/patient/{id}
        [HttpGet("patient/{id}")]
        [SwaggerOperation(Summary = "Tìm kiếm bệnh nhân theo id")]
        public async Task<IActionResult> GetPatientById([FromRoute] Guid id)
        {
            try
            {
                var patient = await _staffReceptionService.SreachPatientAsync(id);
                return Ok(patient);
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

        [HttpGet("patients")]
        [SwaggerOperation(Summary = "Tìm kiếm tất cả bệnh nhân")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _staffReceptionService.ListPatientAsync();
            return Ok(patients);
        }

        [HttpPost("medicalrecords")]
        [SwaggerOperation(Summary = "Tạo hồ sơ bệnh án")]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var recordId = await _staffReceptionService.CreateMediaRecordAsync(dto);
                return Ok(new { message = "Tạo bệnh án thành công", recordId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("AllDoctors")]
        [SwaggerOperation(Summary = "Xem các bác sĩ")]
        public async Task<IActionResult> ListDoctors()
        {
            var doctors = await _staffReceptionService.ListDoctorAsync();
            return Ok(doctors);
        }

        [HttpGet("medicalrecords/patient/{id}")]
        [SwaggerOperation(Summary = "Tìm kiếm hồ sơ bệnh án")]
        public async Task<IActionResult> GetMedicalRecordsByPatientId(Guid id)
        {
            var records = await _staffReceptionService.SreachlistMediaRecordbyId(id);

            if (records == null || !records.Any())
            {
                return NotFound(new { message = "Không tìm thấy hồ sơ bệnh án nào cho bệnh nhân này." });
            }

            return Ok(records);
        }

        [HttpGet("medical-records/{id}")]
        public async Task<IActionResult> GetMedicalRecordDetail(Guid id)
        {
            var result = await _staffReceptionService.DetailMediaRecordbyId(id);
            return Ok(result);
        }

        [HttpPut("medical-records/{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(Guid id, [FromBody] UpdateMedicalRecordDto dto)
        {
            var result = await _staffReceptionService.UpdateMediaRecordbyId(id, dto);
            return Ok(result);
        }

        [HttpDelete("deleteMerdia-Record/{id}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var result = await _staffReceptionService.DeleteMediaRecordbyId(id);
            return Ok(result);
        }
    }
}
