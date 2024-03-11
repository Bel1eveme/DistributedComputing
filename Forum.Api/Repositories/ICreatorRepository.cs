using Forum.Api.Models;
using Forum.Api.Models.Dto;

namespace Forum.Api.Repositories;

public interface ICreatorRepository
{
    public Task<List<Creator>> GetAllAsync();
    
    public Task<Creator?> GetByIdAsync(long id);

    public Task<Creator> CreateAsync(Creator creatorModel);
    
    public Task<Creator?> UpdateAsync(long id, CreatorRequestDto updatedCreator);
    
    public Task<Creator?> DeleteAsync(long id);
}