using MediaStreamingPlatform_API.Domain.Entities;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaPlaylistService
    {
        public Task<string> CreateAndSavePlaylist(MediaPlaylistDto playlist);
        public Task<string> DeletePlaylist(int id);
        public Task<List<MediaPlaylist>> GetAllPlaylists();

    }
}
