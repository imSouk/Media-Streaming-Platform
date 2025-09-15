using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaStreamingPlatform_API.Application.Service
{
    public class PlaylistFileService : IPlaylistFileService
    {
        private readonly IMediaPlaylistService _mediaPlaylistService;

        public PlaylistFileService(IMediaPlaylistService mediaPlaylistService)
        {
            _mediaPlaylistService = mediaPlaylistService;
        }
        public async Task AddPlaylistFile(MediaFile file, int id)
        {
            var playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(id);
            if (playlist == null)
                throw new ArgumentException("Playlist not found");
            file.PlaylistId = playlist.Id;
        }

        public Task RemovePlaylistFile()
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlaylistPositionFile()
        {
            throw new NotImplementedException();
        }
    }
}
