using System.Collections.Concurrent;
using AutoMapper;
using FluentValidation;
using Forum.Api.Kafka;
using Forum.Api.Kafka.Messages;
using Forum.Api.Models;
using Forum.Api.Models.Dto;
using Forum.Api.Repositories;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Forum.Api.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<PostRequestDto> _validator;

    private readonly IKafkaMessageBus<string, KafkaMessage> _messageBus;
    
    private readonly ConcurrentDictionary<string, TaskCompletionSource<IEnumerable<PostResponseDto>>> _responseCompletionSources;

    public PostService(IPostRepository postRepository, IMapper mapper,
        IValidator<PostRequestDto> validator, IKafkaMessageBus<string, KafkaMessage> messageBus,
        ConcurrentDictionary<string, TaskCompletionSource<IEnumerable<PostResponseDto>>> responseCompletionSources)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _validator = validator;
        _messageBus = messageBus;
        _responseCompletionSources = responseCompletionSources;
    }
    
    /*public PostService(IPostRepository postRepository, IMapper mapper,
        IValidator<PostRequestDto> validator)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _validator = validator;
    }*/

    public async Task<IEnumerable<PostResponseDto>> GetAllPosts()
    {
        /*var posts = await _postRepository.GetAllAsync();

        var postResponseDto = _mapper.Map<IEnumerable<PostResponseDto>>(posts);

        return postResponseDto;*/

        var requestKey = Guid.NewGuid().ToString();
        
        var tcs = new TaskCompletionSource<IEnumerable<PostResponseDto>>();
        _responseCompletionSources.TryAdd(requestKey, tcs);

        var message = new KafkaMessage
        {
            MessageType = MessageType.GetAll,
            Data = null
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var postResponseDto = await tcs.Task;
        
        
        
        return postResponseDto.Select(p => new PostResponseDto
        {
            Id = p.Id,
            StoryId = p.StoryId,
            Content = p.Content,
            Story = null
        });
    }

    public async Task<PostResponseDto?> GetPost(long id)
    {
        //generate id
        
        // var posts = await produce
        
        var post = await _postRepository.GetByIdAsync(id);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }

    public async Task<PostResponseDto> CreatePost(PostRequestDto postRequestDto)
    {
        // check story id
        // var posts = await produce
        
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
        // check story id
        // var posts = await produce
        
        var postModel = _mapper.Map<Post>(postRequestDto);
        
        var post = await _postRepository.UpdateAsync(postModel.Id, postModel);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }

    public async Task<PostResponseDto?> DeletePost(long id)
    {
        // var posts = await produce
        
        var post = await _postRepository.DeleteAsync(id);

        return post is not null ? _mapper.Map<PostResponseDto>(post) : null;
    }
}