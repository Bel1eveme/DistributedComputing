using Forum.Api.Models;
using Forum.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Forum.Api.Repositories;

public class CreatorRepository : ICreatorRepository
{
    private readonly AppDbContext _context;

    public CreatorRepository(AppDbContext context)
    {
        _context = context;
        
        SeedDb();
    }

    private void SeedDb()
    {
        var authors = new List<Creator>
        {
            new Creator
            {
                Login = "A.S.Pushkin",
                FirstName ="Alexandr",
                LastName ="Pushkin",
                Password = "1111",
            },
            new Creator
            {
                Login = "M.Y.Lermontov",
                FirstName ="Mihail",
                LastName ="Lermontov",
                Password = "2222",
            },
        };
        
        _context.Creators.AddRange(authors);
        
        _context.SaveChanges();
    }
    

    public async Task<List<Creator>> GetAllAsync()
    {
        return await _context.Creators.ToListAsync();
    }

    public async Task<Creator?> GetByIdAsync(long id)
    {
        return await _context.Creators.FindAsync(id);
    }

    public async Task<Creator> CreateAsync(Creator creatorModel)
    {
        await _context.Creators.AddAsync(creatorModel);
        await _context.SaveChangesAsync();

        return creatorModel;
    }
    

    public async Task<Creator?> UpdateAsync(long id, CreatorRequestDto updatedCreator)
    {
        var existingAuthor = await _context.Creators.FirstOrDefaultAsync(x => x.Id == id);

        if (existingAuthor == null)
        {
            return null;
        }

        existingAuthor.FirstName = updatedCreator.FirstName;
        existingAuthor.LastName = updatedCreator.LastName;
        existingAuthor.Login = updatedCreator.Login;
        existingAuthor.Password = updatedCreator.Password;

        await _context.SaveChangesAsync();
        
        return existingAuthor;
    }

    public async Task<Creator?> DeleteAsync(long id)
    {
        var authorModel = await _context.Creators.FirstOrDefaultAsync(x => x.Id == id);

        if (authorModel == null)
        {
            return null;
        }

        _context.Creators.Remove(authorModel);
        await _context.SaveChangesAsync();

        return authorModel;
    }
}