using JobFinder.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JobFinder.Server.Persistence;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Bookmark> Bookmarks { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Bookmark>(eb =>
        {
            eb.Property(b => b.Content)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<JobAd>(v) ?? new JobAd());
        });
    }
}