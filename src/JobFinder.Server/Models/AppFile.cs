
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Server.Models;

public class AppFile
{
    [Key] public string Uid { get; set; } = null!;
    public byte[] Content { get; set; } = null!;
    public string ContentType { get; set; } = string.Empty;
    public UploadType UploadType { get; set; }
}