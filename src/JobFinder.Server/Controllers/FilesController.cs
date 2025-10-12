
using JobFinder.Server.Models;
using JobFinder.Server.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public FilesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> UploadFile(
        IFormFile file, UploadType type, CancellationToken cancellationToken)
    {
        if (!IsSupported(file, type))
            return BadRequest("not supported or large file");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var appFile = new AppFile
        {
            Uid = Guid.NewGuid().ToString(),
            Content = stream.ToArray(),
            ContentType = file.ContentType,
            UploadType = type
        };

        await _db.AppFiles.AddAsync(appFile);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(appFile.Uid);
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GetFile(string uid)
    {
        var file = await _db.AppFiles.FirstOrDefaultAsync(f => f.Uid == uid);

        if (file == null)
            return NotFound();

        return File(file.Content, file.ContentType, file.Uid);
    }

    private bool IsSupported(IFormFile file, UploadType type)
    {
        var supported = new[] { "image/png", "image/jpeg" };

        return type switch
        {
            UploadType.UserProfile => supported.Any(s => s.ToLower() == file.ContentType.ToLower() && file.Length < 1000000),
            _ => throw new Exception("unknown type")
        };
    }
}
