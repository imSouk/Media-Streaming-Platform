using MediaStreamingPlatform_API.interfaces;

namespace MediaStreamingPlatform_API
{
    public class MediaFileTypeValidator : IMediaFileTypeValidator
    {
        private readonly Dictionary<string, MediaType> _supportedTypes = new()
    {
        { "image/jpeg", MediaType.Image },
        { "image/png", MediaType.Image },
        { "video/mp4", MediaType.Video }
    };

        public MediaType GetMediaType(string contentType) => _supportedTypes.GetValueOrDefault(contentType, MediaType.Unknown);
        public bool IsSupported(string contentType) => _supportedTypes.ContainsKey(contentType);
    }
}
