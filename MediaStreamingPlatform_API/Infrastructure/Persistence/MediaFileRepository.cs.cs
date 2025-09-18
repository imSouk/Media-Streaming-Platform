using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaStreamingPlatform_API.Infrastructure.Persistence
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly MSPContext _context;

        public MediaFileRepository(MSPContext mspContext)
        {
            _context = mspContext;
        }
        public void AddMediaFile(MediaFile mediaFile) => _context.MediaFiles.Add(mediaFile);
        public void DeleteMediaFile(MediaFile mediaFile) => _context.MediaFiles.Remove(mediaFile);

        public async Task<List<MediaFile>> GetAllMediaFiles()
        {
            return await _context.MediaFiles.ToListAsync();
        }
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public async Task<MediaFile?> GetMediaFileById(int id)
        {
            return await _context.MediaFiles.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MediaFile?> GetMediaFileByName(string fileName)
        {
            return await _context.MediaFiles.FirstOrDefaultAsync(m => m.FileName == fileName);
             
        }
    }
}
