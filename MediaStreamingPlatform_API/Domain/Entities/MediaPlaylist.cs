using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaStreamingPlatform_API.Domain.Entities
{
    public class MediaPlaylist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PlaylistName{ get; set; }
        public List<MediaFile>? MediaFiles { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
