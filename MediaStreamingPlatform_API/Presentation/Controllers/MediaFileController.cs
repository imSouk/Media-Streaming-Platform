using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaStreamingPlatform_API.Presentation.Controllers
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
        public async Task<IActionResult> SaveMediaFile(IFormFile file)
        {
            if (file != null && file?.Length > 0)
            {
                var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();
                await _mediaFileService.MapAndSaveMediaFile(file, fileBytes);
                Console.WriteLine(file.ContentType);
                return Ok($"Created {file.FileName}, {file.ContentType}");
            }
            return BadRequest("Cant save ur file.");
        }

        [HttpPost]
        [Route("/{id}")]
        public async Task<IActionResult> DeleteMediaFile( int id)
        {
          if(id > 0)
            {
               var result = await _mediaFileService.DeleteMediaFIleById(id);
                return Ok(result);
            }
            return BadRequest("Cant Delete this Media");
        }

        [HttpGet]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAllMedias()
        {
                var result = await _mediaFileService.GetAllMediaFilesDtos();
                return Ok(result);

        }
        [HttpGet]
        [Route("/GetFileContent{id}")]
        public async Task<FileResult> GetFileConent([FromBody]int id)
        {
           var blob =  _mediaFileService.DeleteMediaFIleById(id);
           if(blob != null)
            {
                return blob;
            }

        }


    }
}
