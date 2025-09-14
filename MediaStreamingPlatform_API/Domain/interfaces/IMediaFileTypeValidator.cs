namespace MediaStreamingPlatform_API.Domain.interfaces
{
    public interface IMediaFileTypeValidator
    {
        MediaType GetMediaType(string contentType);
        bool IsSupported(string contentType);
    }
}
