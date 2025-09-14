using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MediaFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty; 
    public string FilePath { get; set; } = string.Empty;
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