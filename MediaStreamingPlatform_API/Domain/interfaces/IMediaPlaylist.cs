using MediaStreamingPlatform_API.Domain.Entities;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaPlaylistRepository
    {
        public void CreatePlaylist(MediaPlaylist playlist);
        public void DeletePlaylist(MediaPlaylist playlist);
        public MediaPlaylist GetPlaylistById(int id);
        public Task<List<MediaPlaylist>> GetAllPlaylists();
        public Task SaveAsync();
    }
}
