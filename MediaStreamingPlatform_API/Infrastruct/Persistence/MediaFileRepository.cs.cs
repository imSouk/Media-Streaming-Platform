using MediaStreamingPlatform_API.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MediaStreamingPlatform_API.Infrastruct.Persistence
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly MSPContext _MSPContext;
        public MediaFileRepository(MSPContext mspContext)
        {
            _MSPContext = mspContext;
        }
        public string AddMediaFile(MediaFile mediaFile)
        {
            _MSPContext.MediaFiles.Add(mediaFile);
            _MSPContext.SaveChanges();
            return "saved";
        }

        public string DeleteMediaFile(MediaFile mediaFile)
        {
            _MSPContext.MediaFiles.Remove(mediaFile);
            _MSPContext.SaveChanges();
            return "deleted";
        }

        public List<MediaFile> GetAllMediaFiles()
        {
            return _MSPContext.MediaFiles.ToList();
        }

        public async Task<MediaFile> GetMediaFileById(int id)
        {
            MediaFile? media = await _MSPContext.MediaFiles.FirstOrDefaultAsync(m => m.Id == id);
            return media;

        }
    }
}
