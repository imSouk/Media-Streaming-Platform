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

        public void CreatePlaylist(MediaPlaylist playlist) => _MSPContext.Add(playlist);

        public void DeletePlaylist(MediaPlaylist playlist) => _MSPContext?.Remove(playlist);

        public MediaPlaylist GetPlaylistById(int id) => _MSPContext.Playlists.FirstOrDefault(e => e.Id == id);

        public async Task<List<MediaPlaylist>> GetAllPlaylists() =>   await _MSPContext.Playlists.ToListAsync();

        public async Task SaveAsync() => await _MSPContext.SaveChangesAsync();
    }
}
