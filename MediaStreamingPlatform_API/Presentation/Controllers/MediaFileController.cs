using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace MediaStreamingPlatform_API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaFileController : ControllerBase
    {
        private readonly IMediaFileService _mediaFileService;
        public MediaFileController(IMediaFileService mediaFileService)
        {
            _mediaFileService = mediaFileService;
        }
        [HttpPost("save")]

        public async Task<IActionResult> SaveMediaFile(IFormFile file, [FromForm]string playlistId)
        {
            if (file == null && file?.Length < 0)
                return BadRequest("File is required and cannot be empty");
            
            if (!int.TryParse(playlistId, out int id))
                return BadRequest("Invalid playlist ID");
            
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            byte[] fileBytes = ms.ToArray();

            await _mediaFileService.MapAndSaveMediaFile(file, fileBytes, id);
            return Ok($"Created {file.FileName}, {file.ContentType}");

        }

        [HttpDelete("file")]
        public async Task<IActionResult> DeleteMediaFile([FromQuery] string fileName, string playlistId)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Filename is required");
            if (!int.TryParse(playlistId, out int id) && id <= 0)
                return BadRequest("Invalid playlist ID");
            
            var result = await _mediaFileService.DeleteMediaFIleByName(fileName, id);
            return Ok(result);

            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMedias()
        {
                var result = await _mediaFileService.GetAllMediaFilesDtos();
                return Ok(result);

        }
        [HttpPost("content")]
        public async Task<IActionResult> GetFileContent([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return BadRequest("At least one ID is required");
            var blobList = await _mediaFileService.GetBlobById(ids);
            if (blobList == null )
                return NotFound("No files found with the provided IDs");
            return Ok(blobList);
        }
    }
}
