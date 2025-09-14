using Microsoft.AspNetCore.Mvc;

namespace MediaStreamingPlatform_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaFileController : ControllerBase
    {
        private readonly IMediaFileService _mediaFileService;
        public MediaFileController(IMediaFileService mediaFileService)
        {
            _mediaFileService = mediaFileService;
        }
        [HttpPost]
        [Route("/save")]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            if(file != null && file?.Length > 0)
            {

                var uploadsFolder = "uploads";
                Directory.CreateDirectory(uploadsFolder); 
                var path = Path.Combine(uploadsFolder, file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                await _mediaFileService.MapAndSaveMediaFile(file, path);
                Console.WriteLine(file.ContentType);
                return Ok($"Created {file.FileName}, {file.ContentType}");
            }
           return BadRequest("Cant save ur file.");
        }
    }
}
