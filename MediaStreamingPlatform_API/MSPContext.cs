using Microsoft.EntityFrameworkCore;

namespace MediaStreamingPlatform_API
{
    public class MSPContext : DbContext
    {
        public DbSet<MediaFile> MediaFiles{ get; set; }
        public MSPContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
