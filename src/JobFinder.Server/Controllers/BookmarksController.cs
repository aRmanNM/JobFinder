using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookmarksController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly CurrentUserHelper _currentUser;

    public BookmarksController(ApplicationDbContext db, CurrentUserHelper currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bookmark bookmark, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        bookmark.UserId = userId;
        bookmark.Id = 0;
        bookmark.CreatedAt = DateTimeOffset.Now;

        var bookmarks = await _db.Bookmarks.AsNoTracking().Where(b => b.UserId == userId).ToListAsync();
        var exists = bookmarks.Any(b => b.Content.Id == bookmark.Content.Id);

        if (exists)
            return Ok();

        _db.Bookmarks.Add(bookmark);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(bookmark);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Bookmark bookmark, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var currentBookmark = await _db.Bookmarks.FirstOrDefaultAsync(b => b.Id == bookmark.Id && b.UserId == userId);

        if (currentBookmark == null)
            return NotFound();

        currentBookmark.Note = bookmark.Note;
        currentBookmark.LastEditAt = DateTimeOffset.Now;

        await _db.SaveChangesAsync(cancellationToken);

        return Ok(currentBookmark);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int bookmarkId, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var currentBookmark = await _db.Bookmarks.FirstOrDefaultAsync(b => b.Id == bookmarkId && b.UserId == userId);

        if (currentBookmark == null)
            return NotFound();

        _db.Bookmarks.Remove(currentBookmark);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var bookmarks = await _db.Bookmarks
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return Ok(bookmarks);
    }
}