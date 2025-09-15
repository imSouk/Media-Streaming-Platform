namespace MediaStreamingPlatform_API.Application.DTOs
{
    public class MediaPlaylistDto
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public virtual ICollection<MediaFileDto> MediaFiles { get; set; }
        public DateTime UploadedAt { get; set; } 

    }
}
