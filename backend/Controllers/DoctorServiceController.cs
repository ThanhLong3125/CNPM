using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // GET: api/doctor/patients?startDate=2025-01-01&endDate=2025-06-01
        [HttpGet("patients")]
        public async Task<ActionResult<List<PatientDto>>> GetPatientsByCreatedDate([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var patients = await _doctorService.GetPatientsByCreatedDateAsync(startDate, endDate);
            return Ok(patients);
        }

        // GET: api/doctor/priority-patients
        [HttpGet("priority-patients")]
        public async Task<ActionResult<List<MedicalRecordsDto>>> GetPriorityPatients()
        {
            var priorityPatients = await _doctorService.GetPriorityPatientsAsync();
            return Ok(priorityPatients);
        }

        // GET: api/doctor/{patientId}/dicom-images
        [HttpGet("{patientId}/dicom-images")]
        public async Task<ActionResult<List<ImageDICOMDto>>> GetDicomImagesByPatientId(int patientId)
        {
            var images = await _doctorService.GetDicomImagesByPatientIdAsync(patientId);
            return Ok(images);
        }

        // POST: api/doctor/image/{imageId}/comment
        [HttpPost("image/{imageId}/comment")]
        public async Task<ActionResult> AddDoctorComment(Guid imageId, [FromBody] string comment)
        {
            try
            {
                bool result = await _doctorService.AddDoctorCommentAsync(imageId, comment);
                if (result)
                    return Ok(new { message = "Comment added successfully" });
                else
                    return BadRequest(new { message = "Failed to add comment" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
