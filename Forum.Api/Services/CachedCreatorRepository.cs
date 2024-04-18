using Forum.Api.Models;
using Forum.Api.Repositories;

namespace Forum.Api.Services;

public class CachedCreatorRepository : ICreatorRepository
{
    //private readonly ICreatorRepository _creatorRepository;
    
    public Task<List<Creator>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Creator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Creator> CreateAsync(Creator creatorModel)
    {
        throw new NotImplementedException();
    }

    public Task<Creator?> UpdateAsync(long id, Creator updatedCreator)
    {
        throw new NotImplementedException();
    }

    public Task<Creator?> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}