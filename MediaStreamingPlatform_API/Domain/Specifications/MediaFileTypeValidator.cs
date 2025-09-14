using MediaStreamingPlatform_API.Domain.interfaces;

namespace MediaStreamingPlatform_API.Domain.Specifications
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
