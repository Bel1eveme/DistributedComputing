using Forum.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Creator> Creators { get; init; }

    public DbSet<Tag> Tags { get; init; }
    
    public DbSet<Story> Stories { get; init; }
    
    public DbSet<Post> Posts { get; init; }
}