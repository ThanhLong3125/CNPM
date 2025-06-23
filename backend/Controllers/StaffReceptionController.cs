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

        // PUT: api/StaffReception/patient/{idPatient}
        [HttpPut("patient/{idPatient}")]
        [SwaggerOperation(Summary = "Chỉnh sửa hồ sơ bệnh nhân")]
        public async Task<IActionResult> UpdatePatient([FromRoute] string idPatient, [FromBody] UpdatePatientDto dto)
        {
            try
            {
                var updatedPatient = await _staffReceptionService.UpdatePatientAsync(idPatient, dto);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/patient/{idPatient}
        [HttpGet("patient/{idPatient}")]
        [SwaggerOperation(Summary = "Tìm kiếm bệnh nhân theo mã IdPatient")]
        public async Task<IActionResult> GetPatientById([FromRoute] string idPatient)
        {
            try
            {
                var patient = await _staffReceptionService.SreachPatientAsync(idPatient);
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

        // GET: api/StaffReception/patients
        [HttpGet("patients")]
        [SwaggerOperation(Summary = "Lấy danh sách tất cả bệnh nhân")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _staffReceptionService.ListPatientAsync();
            return Ok(patients);
        }

        // POST: api/StaffReception/medicalrecords
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
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/doctors
        [HttpGet("doctors")]
        [SwaggerOperation(Summary = "Lấy danh sách bác sĩ")]
        public async Task<IActionResult> ListDoctors()
        {
            var doctors = await _staffReceptionService.ListDoctorAsync();
            return Ok(doctors);
        }

        // GET: api/StaffReception/medicalrecords/patient/{idPatient}
        [HttpGet("medicalrecords/patient/{idPatient}")]
        [SwaggerOperation(Summary = "Lấy danh sách hồ sơ bệnh án theo IdPatient")]
        public async Task<IActionResult> GetMedicalRecordsByPatientId([FromRoute] string idPatient)
        {
            try
            {
                var records = await _staffReceptionService.SearchMedicalRecordsByPatientId(idPatient);
                if (records == null || !records.Any())
                {
                    return NotFound(new { message = "Không tìm thấy hồ sơ bệnh án nào cho bệnh nhân này." });
                }

                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/medicalrecords/{id}
        [HttpGet("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Xem chi tiết hồ sơ bệnh án")]
        public async Task<IActionResult> GetMedicalRecordDetail([FromRoute] string id)
        {
            try
            {
                var record = await _staffReceptionService.DetailMediaRecordbyId(id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // PUT: api/StaffReception/medicalrecords/{id}
        [HttpPut("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Cập nhật hồ sơ bệnh án")]
        public async Task<IActionResult> UpdateMedicalRecord([FromRoute] string id, [FromBody] UpdateMedicalRecordDto dto)
        {
            try
            {
                var result = await _staffReceptionService.UpdateMediaRecordbyId(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/StaffReception/medicalrecords/{id}
        [HttpDelete("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Xoá hồ sơ bệnh án")]
        public async Task<IActionResult> DeleteRecord([FromRoute] string id)
        {
            try
            {
                var result = await _staffReceptionService.DeleteMediaRecordbyId(id);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/medicalrecords
        [HttpGet("medicalrecords")]
        [SwaggerOperation(Summary = "Lấy tất cả hồ sơ bệnh án")]
        public async Task<IActionResult> ShowMediaRecord()
        {
            var records = await _staffReceptionService.ShowAllMediaRecord();
            return Ok(records);
        }
    }
}
