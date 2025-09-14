namespace MediaStreamingPlatform_API.interfaces
{
    public interface IMediaFileTypeValidator
    {
        MediaType GetMediaType(string contentType);
        bool IsSupported(string contentType);
    }
}
