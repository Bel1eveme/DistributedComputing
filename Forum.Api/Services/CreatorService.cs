using AutoMapper;
using Forum.Api.Models;
using Forum.Api.Models.Dto;
using Forum.Api.Repositories;

namespace Forum.Api.Services;

public class CreatorService : ICreatorService
{
    private readonly ICreatorRepository _creatorRepository;

    private readonly IMapper _mapper;

    public CreatorService(ICreatorRepository creatorRepository, IMapper mapper)
    {
        _creatorRepository = creatorRepository;
        _mapper = mapper;
    }

    public async Task<List<CreatorResponseDto>> GetAllCreators()
    {
        var creators = await _creatorRepository.GetAllAsync();

        var creatorsResponseDto = _mapper.Map<List<CreatorResponseDto>>(creators);

        return creatorsResponseDto;
    }

    public async Task<CreatorResponseDto> GetCreator(long id)
    {
        var creator = await _creatorRepository.GetByIdAsync(id);

        var creatorResponseDto = _mapper.Map<CreatorResponseDto>(creator);

        return creatorResponseDto;
    }

    public async Task<CreatorResponseDto> CreateCreator(CreatorRequestDto creatorRequestDto)
    {
        var creatorModel = _mapper.Map<Creator>(creatorRequestDto);
        
        var creator = await _creatorRepository.CreateAsync(creatorModel);

        var creatorResponseDto = _mapper.Map<CreatorResponseDto>(creator);

        return creatorResponseDto;
    }

    public async Task<CreatorResponseDto> UpdateCreator(CreatorRequestDto creatorRequestDto)
    {
        var creatorModel = _mapper.Map<Creator>(creatorRequestDto);
        
        var creator = await _creatorRepository.UpdateAsync(creatorModel.Id, creatorRequestDto);

        var creatorResponseDto = _mapper.Map<CreatorResponseDto>(creator);

        return creatorResponseDto;
    }

    public async Task<CreatorResponseDto> DeleteCreator(long id)
    {
        var creator = await _creatorRepository.DeleteAsync(id);

        var creatorResponseDto = _mapper.Map<CreatorResponseDto>(creator);

        return creatorResponseDto;
    }
}