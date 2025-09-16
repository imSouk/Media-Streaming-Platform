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
                throw new NullReferenceException("Playlist not found");
            file.PlaylistId = playlist.Id;
        }

        public async Task RemovePlaylistFile(MediaFile file, int id)
        {
            var playlist = await _mediaPlaylistService.GetPlaylistByIdAsync(id);
            if (playlist == null) 
                throw new NullReferenceException("Playlist Not Found");
            playlist.MediaFiles.Remove(file);
        }

        public Task UpdatePlaylistPositionFile()
        {
            throw new NotImplementedException();
        }
    }
}
