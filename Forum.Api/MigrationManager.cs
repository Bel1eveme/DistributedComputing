using Forum.Api.Models;

namespace Forum.Api;

public static class MigrationManager
{
    public static async Task MigrateDatabaseAsync(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        var services = scope.ServiceProvider;

        await using var context = services.GetRequiredService<AppDbContext>();
        
        try
        {
            //await context.Database.MigrateAsync();

            await context.Creators.AddAsync(new Creator
            {
                Id = 45,
                Login = "1231421312",
                Password = "142142141",
                FirstName = "William",
                LastName = "Shakespeare"
            });

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("seeding exception");
                    
            throw;
        }
    }
}