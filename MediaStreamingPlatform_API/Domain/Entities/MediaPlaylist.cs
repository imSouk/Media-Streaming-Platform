using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediaStreamingPlatform_API.Application.DTOs;

namespace MediaStreamingPlatform_API.Domain.Entities
{
    public class MediaPlaylist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PlaylistName{ get; set; }
        public virtual ICollection<MediaFile> MediaFiles { get; set; } = new List<MediaFile>();
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
