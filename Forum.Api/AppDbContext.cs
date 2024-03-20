using Forum.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Creator>().HasData(
            new Creator
            {
                Id = 45,
                Login = "1231421312",
                Password = "142142141",
                FirstName = "William",
                LastName = "Shakespeare"
            }
        );*/
        
        
    }
    

    public DbSet<Creator> Creators { get; init; }

    public DbSet<Tag> Tags { get; init; }
    
    public DbSet<Story> Stories { get; init; }
    
    public DbSet<Post> Posts { get; init; }
}