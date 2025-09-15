using MediaStreamingPlatform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaStreamingPlatform_API.Infrastructure.Persistence
{
    public class MSPContext : DbContext
    {
        public DbSet<MediaPlaylist> Playlists { get; set; }
        public DbSet<MediaFile> MediaFiles{ get; set; }
        public MSPContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
