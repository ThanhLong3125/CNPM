using backend.DTOs;
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

        // Tạo bệnh nhân mới
        [HttpPost("patients")]
        [SwaggerOperation(Summary = "Tạo hồ sơ bệnh nhân mới")]
        [ProducesResponseType(typeof(PatientDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdPatient = await _staffReceptionService.CreatePatientAsync(dto);
                return CreatedAtAction(
                    nameof(GetPatientById),
                    new { idPatient = createdPatient.PatientID },
                    createdPatient
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Cập nhật bệnh nhân
        [HttpPut("patients/{idPatient}")]
        [SwaggerOperation(Summary = "Cập nhật hồ sơ bệnh nhân")]
        public async Task<IActionResult> UpdatePatient(
            [FromRoute] string idPatient,
            [FromBody] UpdatePatientDto dto
        )
        {
            try
            {
                var updatedPatient = await _staffReceptionService.UpdatePatientAsync(
                    idPatient,
                    dto
                );
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // Tìm bệnh nhân theo IdPatient
        [HttpGet("patients/{idPatient}")]
        [SwaggerOperation(Summary = "Tìm kiếm bệnh nhân theo mã IdPatient")]
        public async Task<IActionResult> GetPatientById([FromRoute] string idPatient)
        {
            try
            {
                var patient = await _staffReceptionService.SreachPatientAsync(idPatient);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // Danh sách tất cả bệnh nhân
        [HttpGet("patients")]
        [SwaggerOperation(Summary = "Lấy danh sách tất cả bệnh nhân")]
        public async Task<IActionResult> GetAllPatients()
        {
            var allPatients = await _staffReceptionService.ListPatientAsync();
            return Ok(allPatients);
        }

        // Danh sách bác sĩ
        [HttpGet("doctors")]
        [SwaggerOperation(Summary = "Lấy danh sách bác sĩ")]
        public async Task<IActionResult> ListDoctors()
        {
            var doctors = await _staffReceptionService.ListDoctorAsync();
            return Ok(doctors);
        }

        // Tạo bệnh án
        [HttpPost("medicalrecords")]
        [SwaggerOperation(Summary = "Tạo hồ sơ bệnh án")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdId = await _staffReceptionService.CreateMedicalRecordAsync(dto);
                return CreatedAtAction(
                    nameof(GetMedicalRecordById),
                    new { id = createdId },
                    new { MedicalRecordId = createdId }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Chi tiết bệnh án
        [HttpGet("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Xem chi tiết hồ sơ bệnh án")]
        public async Task<IActionResult> GetMedicalRecordById([FromRoute] string id)
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

        // Danh sách bệnh án theo bệnh nhân
        [HttpGet("medicalrecords/patient/{idPatient}")]
        [SwaggerOperation(Summary = "Lấy danh sách hồ sơ bệnh án theo IdPatient")]
        public async Task<IActionResult> GetMedicalRecordsByPatientId([FromRoute] string idPatient)
        {
            try
            {
                var records = await _staffReceptionService.SearchMedicalRecordsByPatientId(
                    idPatient
                );
                if (records == null || !records.Any())
                    return NotFound(
                        new { message = "Không tìm thấy hồ sơ bệnh án nào cho bệnh nhân này." }
                    );

                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Cập nhật hồ sơ bệnh án
        [HttpPut("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Cập nhật hồ sơ bệnh án")]
        public async Task<IActionResult> UpdateMedicalRecord(
            [FromRoute] string id,
            [FromBody] UpdateMedicalRecordDto dto
        )
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

        // Xoá bệnh án
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

        // Hiển thị tất cả hồ sơ bệnh án
        [HttpGet("medicalrecords")]
        [SwaggerOperation(Summary = "Lấy tất cả hồ sơ bệnh án")]
        public async Task<IActionResult> ShowAllMedicalRecords()
        {
            var records = await _staffReceptionService.ShowAllMedicalRecord();
            return Ok(records);
        }

        [HttpGet("waiting-patients")]
        [SwaggerOperation(Summary = "Lấy danh sách bệnh nhân đang chờ khám")]
        public async Task<IActionResult> GetWaitingPatients()
        {
            var records = await _staffReceptionService.ListWaitingPatientAsync();
            return Ok(records);
        }

        [HttpGet("treated-patients")]
        [SwaggerOperation(Summary = "Lấy danh sách bệnh nhân đã được khám")]
        public async Task<IActionResult> GetTreatedPatients()
        {
            var records = await _staffReceptionService.ListTreatedPatientAsync();
            return Ok(records);
        }
    }
}
