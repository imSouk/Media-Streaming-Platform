namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaFileRepository
    {
        public string AddMediaFile(MediaFile mediaFile);
        public string DeleteMediaFile(MediaFile mediaFile);
        public Task<MediaFile> GetMediaFileById( int id);
        public List<MediaFile> GetAllMediaFiles();
        public  Task SaveAsync();
    }
}
