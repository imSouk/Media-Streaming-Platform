using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaStreamingPlatform_API.Presentation.Controllers
{
    [ApiController]
    [Route("Playlist")]
    public class MediaPlaylistController : ControllerBase
    {
        private readonly IMediaPlaylistService _mediaPlaylistService;
        public MediaPlaylistController(IMediaPlaylistService mediaPlaylistService)
        {
            _mediaPlaylistService = mediaPlaylistService;
        }

        [HttpPost]
        [Route("/Create")]
        public async Task<IActionResult> CreatePlaylist([FromQuery]string name)
        {
            if(name != null) 
            {
                MediaPlaylistCreateDto mediaPlaylistDto = new MediaPlaylistCreateDto();
                mediaPlaylistDto.PlaylistName = name;
                string result = await _mediaPlaylistService.CreateAndSavePlaylist(mediaPlaylistDto);
                return Ok(result);
                
            }
            return Ok($"Cant Create playlist with name{name}");
        }

        [HttpPost]
        [Route("/Delete")]
        public async Task<IActionResult> DeletePlaylist([FromQuery] int id)
        {
            if (id > 0 )
            {
               string result = await _mediaPlaylistService.DeletePlaylist(id);
                return Ok(result);

            }
            return Ok($"Cant Delete the playlist with id -> {id}");
        }

        [HttpGet]
        [Route("/GetAllPlaylists")]
        public async Task<List<MediaPlaylistDto>> GetAllPlaylists()
        {
            
            var result = await _mediaPlaylistService.GetAllPlaylists();
            return result;            
        }

        [HttpGet]
        [Route("/GetPlaylistById")]
        public async Task<MediaPlaylist> GetPlaylistById([FromQuery]int id)
        {
            var result = await _mediaPlaylistService.GetPlaylistByIdAsync(id);
            return result;
        }



    }
}
