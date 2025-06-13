// Controllers/ImageController.cs
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // Important for file uploads
        public async Task<IActionResult> Upload([FromForm] UploadImageDto uploadImageDto)
        {
            if (uploadImageDto == null || uploadImageDto.File == null)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                var uploadedImage = await _imageService.UploadImageAsync(uploadImageDto);
                return Ok(uploadedImage); // Return the saved image details
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework like Serilog or NLog)
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return StatusCode(500, "Internal server error during image upload.");
            }
        }

        // You might also want an endpoint to retrieve an image (e.g., by ID or path)
        // For example:
        // [HttpGet("{imageId}")]
        // public IActionResult GetImage(Guid imageId)
        // {
        //     // Logic to retrieve the image path from DB and return the file
        //     // ...
        // }
    }
}
