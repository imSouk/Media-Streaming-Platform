using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaPlaylistRepository
    {
        public void CreatePlaylist(MediaPlaylist playlist);
        public void DeletePlaylist(MediaPlaylist playlist);
        public Task<MediaPlaylist> GetPlaylistByIdAsync(int id);
        public Task<MediaPlaylistDto> GetPlaylistItems(int id);
        public Task<List<MediaPlaylistDto>> GetAllPlaylistsWithFiles();

        public Task SaveAsync();
    }
}
