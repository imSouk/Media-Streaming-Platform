namespace MediaStreamingPlatform_API
{
    public interface IMediaFileService
    {
        public Task MapAndSaveMediaFile(IFormFile formFile, string path);

    }
}
