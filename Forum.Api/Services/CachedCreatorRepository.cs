using Forum.Api.Models;
using Forum.Api.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Forum.Api.Services;

public class CachedCreatorRepository : ICreatorRepository
{
    private readonly ICreatorRepository _creatorRepository;

    private readonly IDistributedCache _cache;

    public CachedCreatorRepository(ICreatorRepository creatorRepository, IDistributedCache cache)
    {
        _creatorRepository = creatorRepository;
        _cache = cache;
    }

    public async Task<List<Creator>> GetAllAsync()
    {
        var cachedCreators = await _cache.GetStringAsync("creators");
        if (!string.IsNullOrEmpty(cachedCreators))
            return JsonSerializer.Deserialize<List<Creator>>(cachedCreators);

        var creators = await _creatorRepository.GetAllAsync();
        await _cache.SetStringAsync("creators", JsonSerializer.Serialize(creators));
        return creators;
    }

    public async Task<Creator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Creator> CreateAsync(Creator creatorModel)
    {
        throw new NotImplementedException();
    }

    public async Task<Creator?> UpdateAsync(long id, Creator updatedCreator)
    {
        throw new NotImplementedException();
    }

    public async Task<Creator?> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}