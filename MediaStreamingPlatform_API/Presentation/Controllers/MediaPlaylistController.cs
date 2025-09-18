using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaStreamingPlatform_API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaPlaylistController : ControllerBase
    {
        private readonly IMediaPlaylistService _mediaPlaylistService;
        public MediaPlaylistController(IMediaPlaylistService mediaPlaylistService)
        {
            _mediaPlaylistService = mediaPlaylistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromQuery]string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Playlist name is required");

                var mediaPlaylistDto = new MediaPlaylistCreateDto
                {
                    PlaylistName = name
                };
            string result = await _mediaPlaylistService.CreateAndSavePlaylist(mediaPlaylistDto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlaylist([FromQuery] string playlistId)
        {
            int.TryParse(playlistId, out int id);
            if (id <= 0)
                return BadRequest("Invalid playlist ID");            

            string result = await _mediaPlaylistService.DeletePlaylist(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<List<MediaPlaylistDto>> GetAllPlaylists()
        {
            
            var result = await _mediaPlaylistService.GetAllPlaylists();
            return result;            
        }

        [HttpGet]
        [Route("/ById")]
        public async Task<ActionResult<MediaPlaylistDto>> GetPlaylistById([FromQuery]int id)
        {
            if (id <= 0)
                return BadRequest("Invalid playlist ID");
            var result = await _mediaPlaylistService.GetPlaylistItemsByIdAsync(id);
            if (result == null)
                return NotFound($"Playlist with ID {id} not found");
            return Ok(result);
        }

        [HttpPost]
        [Route("/start")]
        public async Task<IActionResult> StartPlaylist([FromQuery] string playlistId)
        {
            int.TryParse(playlistId, out int id);
            if (id <= 0)
                return BadRequest("Invalid playlist ID");            
            var result = await _mediaPlaylistService.SendPlayCommand(id);
            return Ok(result);
        }

    }
}
