namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaFileRepository
    {
        public void AddMediaFile(MediaFile mediaFile);
        public void DeleteMediaFile(MediaFile mediaFile);
        public Task<MediaFile> GetMediaFileByName(string fileName);
        public Task<MediaFile> GetMediaFileById( int id);
        public Task<List<MediaFile>> GetAllMediaFiles();
        public  Task SaveAsync();
    }
}
