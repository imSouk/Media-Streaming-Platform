using MediaStreamingPlatform_API.Application.DTOs;
using MediaStreamingPlatform_API.Domain.Entities;
using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MediaStreamingPlatform_API.Infrastructure.Persistence
{
    public class MediaPlaylistRepository : IMediaPlaylistRepository
    {
        private readonly MSPContext _context;
        public MediaPlaylistRepository(MSPContext mspContext)
        {
            _context = mspContext;
        }

        public void CreatePlaylist(MediaPlaylist playlist) => _context.Playlists.Add(playlist);

        public void DeletePlaylist(MediaPlaylist playlist) => _context.Playlists.Remove(playlist);

        public async Task<MediaPlaylist?> GetPlaylistByIdAsync(int id) => await _context.Playlists.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<MediaPlaylistDto>> GetAllPlaylistsWithFiles()
        {
            return await _context.Playlists
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
        public async Task<MediaPlaylistDto?> GetPlaylistItems(int id)
        {
            return await _context.Playlists.Where(p => p.Id == id).Select(p => new MediaPlaylistDto
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

        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
