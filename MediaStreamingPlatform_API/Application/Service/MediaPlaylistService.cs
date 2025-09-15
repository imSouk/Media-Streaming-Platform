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
        public async Task<string> CreateAndSavePlaylist(MediaPlaylistDto playlistDto)
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
                MediaPlaylist playlist = _mediaPlaylistService.GetPlaylistById(id);
                if (playlist != null)
                {
                    _mediaPlaylistService.DeletePlaylist(playlist);
                    await _mediaPlaylistService.SaveAsync();
                    return $"Playlist delete {playlist.Id} {playlist.PlaylistName}";
                }
                return $"Playlist with id {id} not found";
           
        }

        public async Task<List<MediaPlaylist>> GetAllPlaylists()
        {
            List<MediaPlaylist> playlist = await _mediaPlaylistService.GetAllPlaylists();
            return playlist;
        }
    }
}
