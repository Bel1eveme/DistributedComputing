using AutoMapper;
using Forum.Api.Models;
using Forum.Api.Models.Dto;
using Forum.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Forum.Api.Controllers;

[Route("api/v1.0/creator")]
[ApiController]
public class CreatorController : ControllerBase
{
    private readonly ICreatorService _creatorService;

    private readonly IMapper _mapper;

    public CreatorController(ICreatorService creatorService, IMapper mapper)
    {
        _creatorService = creatorService;
        _mapper = mapper;
    }

    [HttpGet]
    public List<CreatorResponseDto> Get()
    {
        List<CreatorResponseDto>? list;

        try
        {
            list = _creatorService.GetAllCreators().Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return list;
    }
    
    [HttpGet("{id:long}")]
    public async Task<List<CreatorResponseDto>> Get(long id)
    {
        List<CreatorResponseDto>? list;

        try
        {
            list = await _creatorService.GetCreator(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return list;
    }
}