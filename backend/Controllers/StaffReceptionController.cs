using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _staffReceptionService.ListPatientAsync();
            return Ok(patients);
        }

        [HttpPost("medicalrecords")]
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
        public async Task<IActionResult> ListDoctors()
        {
            var doctors = await _staffReceptionService.ListUserAsync();
            return Ok(doctors);
        }
    }
}
