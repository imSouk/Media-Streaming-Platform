using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MediaStreamingPlatform_API.Infrastructure.Persistence
{
    public class MediaPlaylistRepository : IMediaPlaylistRepository
    {
        private readonly MSPContext _MSPContext;
        public MediaPlaylistRepository(MSPContext mspContext)
        {
            _MSPContext = mspContext;
        }

        public void CreatePlaylist(MediaPlaylist playlist) => _MSPContext.Playlists.Add(playlist);

        public void DeletePlaylist(MediaPlaylist playlist) => _MSPContext?.Playlists.Remove(playlist);
        public async Task UpdatePlaylistAsync(MediaPlaylist playlist) => _MSPContext?.Playlists.Update(playlist);

        public async Task<MediaPlaylist> GetPlaylistByIdAsync(int id) => await _MSPContext.Playlists.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<MediaPlaylistDto>> GetAllPlaylistsWithFiles()
        {
            return await _MSPContext.Playlists
                .Select(p => new MediaPlaylistDto
                {
                    Id = p.Id,
                    PlaylistName = p.PlaylistName,
                    UploadedAt = p.UploadedAt,
                    MediaFiles = p.MediaFiles.Select(m => new MediaFileDto
                    {
                        Id = m.Id,
                        FileName = m.FileName,
                        ContentType = m.ContentType,
                        FileSize = m.FileSize,
                        Type = m.Type
                       
                    }).ToList()
                })
                .ToListAsync();
        }
        public async Task<MediaPlaylistDto> GetPlaylistItems(int id)
        {
            MediaPlaylistDto? playlist =  await _MSPContext.Playlists.Where(p => p.Id == id).Select(p => new MediaPlaylistDto
            {
                Id = p.Id,
                PlaylistName = p.PlaylistName,
                UploadedAt = p.UploadedAt,
                MediaFiles = p.MediaFiles.Select(m => new MediaFileDto
                {
                    Id = m.Id,
                    FileName = m.FileName,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    Type = m.Type

                }).ToList()
            }).FirstOrDefaultAsync();
            return playlist;
        }

        public async Task SaveAsync() => await _MSPContext.SaveChangesAsync();
    }
}
