using FluentValidation;
using Forum.PostApi.Models;
using Forum.PostApi.Models.Dto;
using Forum.PostApi.Repositories;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Forum.PostApi.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<PostRequestDto> _validator;

    public PostService(IPostRepository postRepository, IMapper mapper, IValidator<PostRequestDto> validator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<List<PostResponseDto>> GetAllPosts()
    {
        var posts = await _postRepository.GetAllAsync();

        var postResponseDto = _mapper.Map<List<PostResponseDto>>(posts);

        return postResponseDto;
    }

    public async Task<PostResponseDto?> GetPost(long id)
    {
        var post = await _postRepository.GetByIdAsync(id);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }

    public async Task<PostResponseDto> CreatePost(PostRequestDto postRequestDto)
    {
        var validationResult = await _validator.ValidateAsync(postRequestDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        var postModel = _mapper.Map<Post>(postRequestDto);
        
        var post = await _postRepository.CreateAsync(postModel);

        var postResponseDto = _mapper.Map<PostResponseDto>(post);

        return postResponseDto;
    }

    public async Task<PostResponseDto?> UpdatePost(PostRequestDto postRequestDto)
    {
        var postModel = _mapper.Map<Post>(postRequestDto);
        
        var post = await _postRepository.UpdateAsync(postModel.Id, postModel);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }

    public async Task<PostResponseDto?> DeletePost(long id)
    {
        var post = await _postRepository.DeleteAsync(id);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }
}