// backend/Controllers/DoctorController.cs
using System.Security.Claims; // Needed for ClaimTypes
using backend.DTOs;
using backend.Filters; // If you put your filter in this namespace
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    [ServiceFilter(typeof(CustomExceptionFilter))] // Apply the filter to this controller
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // Helper method to get doctor ID and handle Unauthorized case
        private Guid GetDoctorIdFromToken()
        {
            var doctorIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(doctorIdClaim))
            {
                // Throw an exception that the filter can catch and turn into Unauthorized
                throw new UnauthorizedAccessException("Doctor ID not found in token.");
            }
            return Guid.Parse(doctorIdClaim);
        }

        /// <summary>
        /// Get a specific medical record for a doctor.
        /// </summary>
        [HttpGet("medicalrecords/{medicalRecordId}")]
        public async Task<IActionResult> GetMedicalRecord(Guid medicalRecordId)
        {
            var doctorId = GetDoctorIdFromToken();
            var medicalRecord = await _doctorService.GetMedicalRecordForDoctorAsync(
                medicalRecordId,
                doctorId
            );
            // The service is expected to return null if not found/accessible,
            // or throw a KeyNotFoundException/UnauthorizedAccessException if it wants the filter to handle it.
            if (medicalRecord == null)
            {
                return NotFound(
                    $"Medical record with ID {medicalRecordId} not found or not accessible by this doctor."
                );
            }
            return Ok(medicalRecord);
        }

        /// <summary>
        /// Get medical records assigned to the currently authenticated doctor.
        /// </summary>
        [HttpGet("medicalrecords/assigned")]
        public async Task<IActionResult> GetAssignedMedicalRecords()
        {
            var doctorId = GetDoctorIdFromToken();
            var medicalRecords = await _doctorService.GetAssignedMedicalRecordsAsync(doctorId);
            return Ok(medicalRecords);
        }

        /// <summary>
        /// Submit a diagnosis report for a specific medical record.
        /// </summary>
        [HttpPost("medicalrecords/{medicalRecordId}/diagnosis")]
        public async Task<IActionResult> SubmitDiagnosis(
            Guid medicalRecordId,
            [FromBody] SubmitDiagnosisDto submitDiagnosisDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorId = GetDoctorIdFromToken();
            var updatedRecord = await _doctorService.SubmitDiagnosisAsync(
                medicalRecordId,
                submitDiagnosisDto,
                doctorId
            );
            return Ok(updatedRecord);
        }

        /// <summary>
        /// Get images associated with a medical record.
        /// </summary>
        [HttpGet("medicalrecords/{medicalRecordId}/images")] // Or /imageanalysisrequests if returning DTOs
        public async Task<IActionResult> GetImagesForMedicalRecord(Guid medicalRecordId)
        {
            // If GetImagesForMedicalRecordAsync is for actual images, use it directly.
            // If you changed your IDoctorService to use GetImageAnalysisRequestsByMedicalRecordAsync,
            // then you'd call that here and update the return type.
            var images = await _doctorService.GetImagesForMedicalRecordAsync(medicalRecordId);
            return Ok(images);
        }
    }
}
