// Controllers/ImageController.cs
using System.Net; // For HttpStatusCode
using backend.DTOs;
using backend.Exceptions; // Make sure to include your custom exceptions
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [SwaggerOperation(Summary = "upload ảnh")]
        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // Essential for handling file uploads
        [ProducesResponseType(typeof(ImageDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Upload([FromForm] UploadImageDto uploadImageDto)
        {
            // Basic validation for the incoming file
            if (
                uploadImageDto == null
                || uploadImageDto.File == null
                || uploadImageDto.File.Length == 0
            )
            {
                return BadRequest("No file or an empty file was provided.");
            }

            try
            {
                var uploadedImage = await _imageService.UploadImageAsync(uploadImageDto);
                // Return 201 Created if you're returning the resource that was just created.
                // For simplicity, sticking with Ok for now, but 201 is often semantically better for creation.
                return Ok(uploadedImage);
            }
            catch (NotFoundException ex)
            {
                // Catches specific NotFoundException (e.g., if DiagnosisId is not found)
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors during the upload process
                Console.WriteLine($"Error uploading image: {ex.Message}");
                // In production, avoid exposing raw exception messages.
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    "An error occurred during image upload. Please try again."
                );
            }
        }

        [SwaggerOperation(Summary = "Xem thông tin ảnh theo ID")]
        [HttpGet("{imageId}")] // Route constraint to ensure imageId is a GUID
        [ProducesResponseType(typeof(Image), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetImage(string imageId)
        {
            try
            {
                var imageDto = await _imageService.GetImageByIdAsync(imageId);
                return Ok(imageDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving image {imageId}: {ex.Message}");
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    "An error occurred while retrieving image details."
                );
            }
        }

        [SwaggerOperation(Summary = "phân tích ảnh bằng AI")]
        [HttpPost("{imageId}/analyze")] // Using POST as it performs an action (analysis)
        [ProducesResponseType(typeof(ImageDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)] // Indicating external service issues
        public async Task<IActionResult> AnalyzeImage(string imageId)
        {
            try
            {
                var analyzedImage = await _imageService.AnalyzeImageAsync(imageId);
                return Ok(analyzedImage);
            }
            catch (NotFoundException ex)
            {
                // This could be if the image or its physical file is not found
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                // Catch ApplicationException for errors originating from the AI service itself (e.g., API key, connectivity)
                Console.WriteLine($"AI analysis error for image {imageId}: {ex.Message}");
                // Return 503 Service Unavailable if it's an external service issue
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, ex.Message);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"Error analyzing image {imageId}: {ex.Message}");
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    "An unexpected error occurred during AI analysis."
                );
            }
        }

        [SwaggerOperation(Summary = "Soft delete image")]
        [HttpDelete("{imageId}")] // Using HTTP DELETE verb
        [ProducesResponseType((int)HttpStatusCode.NoContent)] // Standard for successful deletion without content
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteImage(string imageId)
        {
            try
            {
                await _imageService.DeleteImageAsync(imageId);
                return NoContent(); // 204 No Content for successful deletion
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image {imageId}: {ex.Message}");
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    "An error occurred during image deletion."
                );
            }
        }
    }
}
