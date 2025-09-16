using MediaStreamingPlatform_API.Application.DTOs;

namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IPlaylistFileService
    {
        public Task AddPlaylistFile(MediaFile file, int id);
        public Task RemovePlaylistFile(MediaFile file, int id);
        public Task UpdatePlaylistPositionFile();
    }
}
