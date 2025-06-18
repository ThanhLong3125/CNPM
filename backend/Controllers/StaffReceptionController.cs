// Controllers/StaffReceptionController.cs
using backend.DTOs;
using backend.Exceptions; // ADDED: To catch specific exceptions from the service
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

        // =================================================================
        // Patient Endpoints
        // =================================================================

        // POST: api/StaffReception/patients
        [HttpPost("patients")] // MODIFIED: Route for consistency
        [SwaggerOperation(Summary = "Tạo hồ sơ bệnh nhân mới (Create a new patient record)")]
        [ProducesResponseType(typeof(PatientDto), 201)] // MODIFIED: Documenting correct response
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // MODIFIED: The service returns the full PatientDto object.
                var createdPatient = await _staffReceptionService.CreatePatientAsync(dto);
                // MODIFIED: Returning a 201 CreatedAtAction is RESTful best practice.
                // It provides the new resource's location in the response header.
                return CreatedAtAction(
                    nameof(GetPatientById),
                    new { id = createdPatient.Id },
                    createdPatient
                );
            }
            catch (ConflictException ex) // ADDED: Specific exception handling
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/StaffReception/patients/{id}
        [HttpPut("patients/{id}")] // MODIFIED: More RESTful route
        [SwaggerOperation(
            Summary = "Cập nhật hồ sơ bệnh nhân hiện có (Update an existing patient record)"
        )]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdatePatient(
            [FromRoute] Guid id,
            [FromBody] UpdatePatientDto dto
        ) // MODIFIED: id comes FromRoute
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedPatient = await _staffReceptionService.UpdatePatientAsync(id, dto);
                return Ok(updatedPatient);
            }
            catch (NotFoundException ex) // MODIFIED: Catching the specific exception from the service
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // ADDED: Endpoint for SoftDeletePatientAsync
        // DELETE: api/StaffReception/patients/{id}
        [HttpDelete("patients/{id}")]
        [SwaggerOperation(Summary = "Xóa tạm thời hồ sơ bệnh nhân (Soft delete a patient record)")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> SoftDeletePatient(Guid id)
        {
            try
            {
                await _staffReceptionService.SoftDeletePatientAsync(id);
                return NoContent(); // 204 No Content is the standard response for a successful delete
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/patients/{id}
        [HttpGet("patients/{id}")] // MODIFIED: Route for consistency
        [SwaggerOperation(Summary = "Tìm bệnh nhân theo ID (Get a patient by their ID)")]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            try
            {
                var patient = await _staffReceptionService.GetPatientByIdAsync(id);
                return Ok(patient);
            }
            catch (NotFoundException ex) // MODIFIED: Catching the specific exception
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // GET: api/StaffReception/patients
        [HttpGet("patients")]
        [SwaggerOperation(
            Summary = "Liệt kê tất cả bệnh nhân hoặc tìm kiếm theo một thuật ngữ (List all patients or search by a term)"
        )]
        [ProducesResponseType(typeof(List<PatientDto>), 200)]
        // MODIFIED: This single endpoint now handles both listing all and searching
        public async Task<IActionResult> SearchAndListPatients([FromQuery] string? searchTerm)
        {
            // If a search term is provided, use the search service method.
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchedPatients = await _staffReceptionService.SearchPatientsAsync(searchTerm);
                return Ok(searchedPatients);
            }

            // Otherwise, list all patients.
            var allPatients = await _staffReceptionService.ListAllPatientsAsync();
            return Ok(allPatients);
        }

        // =================================================================
        // Medical Record Endpoints
        // =================================================================

        // POST: api/StaffReception/medicalrecords
        [HttpPost("medicalrecords")]
        [SwaggerOperation(
            Summary = "Tạo hồ sơ y tế cho bệnh nhân (Create a medical record for a patient)"
        )]
        [ProducesResponseType(typeof(MedicalRecordDto), 201)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // MODIFIED: The service returns the full MedicalRecordDto
                var createdRecord = await _staffReceptionService.CreateMedicalRecordAsync(dto);
                return CreatedAtAction(
                    nameof(GetMedicalRecordById),
                    new { id = createdRecord.Id },
                    createdRecord
                );
            }
            catch (NotFoundException ex) // MODIFIED: Catch specific exception
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // ADDED: Endpoint for GetMedicalRecordByIdAsync
        // GET: api/StaffReception/medicalrecords/{id}
        [HttpGet("medicalrecords/{id}")]
        [SwaggerOperation(
            Summary = "Tìm hồ sơ y tế cụ thể theo ID (Get a specific medical record by its ID)"
        )]
        [ProducesResponseType(typeof(MedicalRecordDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetMedicalRecordById(Guid id)
        {
            try
            {
                var record = await _staffReceptionService.GetMedicalRecordByIdAsync(id);
                return Ok(record);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // ADDED: Endpoint for ListMedicalRecordsByPatientIdAsync
        // GET: api/StaffReception/patients/{patientId}/medicalrecords
        [HttpGet("patients/{patientId}/medicalrecords")] // MODIFIED: More RESTful route showing relationship
        [SwaggerOperation(
            Summary = "Tìm tất cả hồ sơ y tế của một bệnh nhân cụ thể (Get all medical records for a specific patient)"
        )]
        [ProducesResponseType(typeof(List<MedicalRecordDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetMedicalRecordsByPatientId(Guid patientId)
        {
            try
            {
                var records = await _staffReceptionService.ListMedicalRecordsByPatientIdAsync(
                    patientId
                );
                // It's fine to return an empty list if the patient exists but has no records.
                // The service throws a NotFoundException if the PATIENT does not exist.
                return Ok(records);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // ADDED: Endpoint for UpdateMedicalRecordAsync
        // PUT: api/StaffReception/medicalrecords/{id}
        [HttpPut("medicalrecords/{id}")]
        [SwaggerOperation(
            Summary = "Cập nhật hồ sơ y tế hiện có (Update an existing medical record)"
        )]
        [ProducesResponseType(typeof(MedicalRecordDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateMedicalRecord(
            Guid id,
            [FromBody] UpdateMedicalRecordDto dto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRecord = await _staffReceptionService.UpdateMedicalRecordAsync(id, dto);
                return Ok(updatedRecord);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // ADDED: Endpoint for SoftDeleteMedicalRecordAsync
        // DELETE: api/StaffReception/medicalrecords/{id}
        [HttpDelete("medicalrecords/{id}")]
        [SwaggerOperation(Summary = "Xóa mềm thời hồ sơ y tế (Soft delete a medical record)")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> SoftDeleteMedicalRecord(Guid id)
        {
            try
            {
                await _staffReceptionService.SoftDeleteMedicalRecordAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // =================================================================
        // Doctor Endpoints
        // =================================================================

        // GET: api/StaffReception/doctors
        [HttpGet("doctors")] // MODIFIED: More conventional route name
        [SwaggerOperation(Summary = "Lấy danh sách tất cả bác sĩ (Get a list of all doctors)")]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        public async Task<IActionResult> ListDoctors()
        {
            var doctors = await _staffReceptionService.ListDoctorAsync();
            return Ok(doctors);
        }
    }
}
