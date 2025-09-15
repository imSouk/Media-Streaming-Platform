using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;

namespace MediaStreamingPlatform_API.Application.Service
{
    public class MediaPlaylistService : IMediaPlaylistService
    {
        private readonly IMediaPlaylistRepository _mediaPlaylistService;
        public MediaPlaylistService(IMediaPlaylistRepository repository)
        {
            _mediaPlaylistService = repository;
        }
        public async Task<string> CreateAndSavePlaylist(MediaPlaylistCreateDto playlistDto)
        {
            if (playlistDto != null)
            {
                MediaPlaylist playlist = new MediaPlaylist();
                playlist.PlaylistName = playlistDto.PlaylistName;
                _mediaPlaylistService.CreatePlaylist(playlist);
                await _mediaPlaylistService.SaveAsync();
                return $"Playlist created {playlist.PlaylistName}";
            }
            return $"Cant create ur playlist, check all of the needed parameters";
        }
        public async Task<string> DeletePlaylist(int id)
        {
                MediaPlaylist playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(id);
                if (playlist != null)
                {
                    _mediaPlaylistService.DeletePlaylist(playlist);
                    await _mediaPlaylistService.SaveAsync();
                    return $"Playlist delete {playlist.Id} {playlist.PlaylistName}";
                }
                return $"Playlist with id {id} not found";
           
        }

        public async Task<List<MediaPlaylistDto>> GetAllPlaylists()
        {
            List<MediaPlaylistDto> playlist = await _mediaPlaylistService.GetAllPlaylistsWithFiles();
            return playlist;
        }
        public async Task<MediaPlaylist> GetPlaylistByIdAsync(int id)
        {
            MediaPlaylist playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(id);
            return playlist;
        }
        
    }
}
