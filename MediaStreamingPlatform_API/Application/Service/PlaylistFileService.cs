using MediaStreamingPlatform_API.Domain.interfaces;


namespace MediaStreamingPlatform_API.Application.Service
{
    public class PlaylistFileService : IPlaylistFileService
    {
        private readonly IMediaPlaylistService _mediaPlaylistService;

        public PlaylistFileService(IMediaPlaylistService mediaPlaylistService)
        {
            _mediaPlaylistService = mediaPlaylistService;
        }
        public async Task AddPlaylistFile(MediaFile file, int playlistId)
        {
            var playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(playlistId);
            if (playlist == null)
                throw new ArgumentException($"Playlist with ID {playlistId} not found");
            file.PlaylistId = playlist.Id;
        }

        public async Task RemovePlaylistFile(MediaFile file, int playlistId)
        {
            var playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(playlistId);
            if (playlist == null)
                throw new ArgumentException($"Playlist with ID {playlistId} not found");
            playlist.MediaFiles?.Remove(file);
        }
        public Task UpdatePlaylistPositionFile()
        {
            throw new NotImplementedException();
        }
    }
}
