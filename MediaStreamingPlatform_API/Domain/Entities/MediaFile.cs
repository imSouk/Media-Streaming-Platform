using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MediaFile
{
    [Key]
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }        
    public MediaType Type { get; set; }       
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

public enum MediaType
{
    Unknown,
    Image,
    Video
}