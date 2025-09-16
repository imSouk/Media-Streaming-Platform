using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaPlaylistService
    {
        public Task<string> CreateAndSavePlaylist(MediaPlaylistCreateDto playlist);
        public Task<string> DeletePlaylist(int id);
        public Task<MediaPlaylist> GetPlaylistByIdAsync(int id);
        public Task<List<MediaPlaylistDto>> GetAllPlaylists();
        public Task<MediaPlaylistDto> GetPlaylistItemsByIdAsync(int id);
        public Task<string> SendPlayCommand(int id);

    }
}
