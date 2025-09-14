
namespace MediaStreamingPlatform_API
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
            return ("saved");
        }
    }
}
