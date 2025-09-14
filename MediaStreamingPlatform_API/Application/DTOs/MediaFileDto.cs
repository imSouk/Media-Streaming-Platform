
namespace MediaStreamingPlatform_API.Application.DTOs
{
    public class MediaFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public MediaType Type { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
