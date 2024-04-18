using System.Collections.Concurrent;
using AutoMapper;
using FluentValidation;
using Forum.Api.Exceptions;
using Forum.Api.Kafka;
using Forum.Api.Kafka.Messages;
using Forum.Api.Models;
using Forum.Api.Models.Dto;
using Forum.Api.Repositories;
using Newtonsoft.Json;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Forum.Api.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<PostRequestDto> _validator;

    private readonly IKafkaMessageBus<string, KafkaMessage> _messageBus;
    
    private readonly ConcurrentDictionary<string, TaskCompletionSource<KafkaMessage>> _responseCompletionSources;

    public PostService(IPostRepository postRepository, IMapper mapper,
        IValidator<PostRequestDto> validator, IKafkaMessageBus<string, KafkaMessage> messageBus,
        ConcurrentDictionary<string, TaskCompletionSource<KafkaMessage>> responseCompletionSources)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _validator = validator;
        _messageBus = messageBus;
        _responseCompletionSources = responseCompletionSources;
    }

    public async Task<IEnumerable<PostResponseDto>> GetAllPosts()
    {
        var requestKey = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<KafkaMessage>();
        _responseCompletionSources.TryAdd(requestKey, tcs);

        var message = new KafkaMessage
        {
            MessageType = MessageType.GetAll,
            Data = null
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var kafkaMessage = await tcs.Task;

        if (kafkaMessage.ErrorOccured)
        {
            throw new KafkaException(kafkaMessage.ErrorMessage);
        }
        
        var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(kafkaMessage.Data);
        
        return _mapper.Map<IEnumerable<PostResponseDto>>(posts);
    }

    public async Task<PostResponseDto?> GetPost(long id)
    {
        var requestKey = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<KafkaMessage>();
        _responseCompletionSources.TryAdd(requestKey, tcs);

        var message = new KafkaMessage
        {
            MessageType = MessageType.GetById,
            Data = JsonConvert.SerializeObject(id)
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var kafkaMessage = await tcs.Task;

        if (kafkaMessage.Data == null)
            return null;
        
        if (kafkaMessage.ErrorOccured)
        {
            throw new KafkaException(kafkaMessage.ErrorMessage);
        }
        
        var posts = JsonConvert.DeserializeObject<Post>(kafkaMessage.Data);
        
        return _mapper.Map<PostResponseDto>(posts);
    }

    public async Task<PostResponseDto> CreatePost(PostRequestDto postRequestDto)
    {
        var validationResult = await _validator.ValidateAsync(postRequestDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        var requestKey = Guid.NewGuid().ToString();
        var newId = new Random().Next();
        postRequestDto.Id = newId;
        var tcs = new TaskCompletionSource<KafkaMessage>();
        _responseCompletionSources.TryAdd(requestKey, tcs);
        var newPost = _mapper.Map<Post>(postRequestDto);

        var message = new KafkaMessage
        {
            MessageType = MessageType.Create,
            Data = JsonConvert.SerializeObject(newPost)
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var kafkaMessage = await tcs.Task;

        if (kafkaMessage.Data == null)
            throw new Exceptions.ValidationException();
        
        if (kafkaMessage.ErrorOccured)
        {
            throw new KafkaException(kafkaMessage.ErrorMessage);
        }
        
        var post = JsonConvert.DeserializeObject<Post>(kafkaMessage.Data);
        
        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<PostResponseDto?> UpdatePost(PostRequestDto postRequestDto)
    {
        var validationResult = await _validator.ValidateAsync(postRequestDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        var requestKey = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<KafkaMessage>();
        _responseCompletionSources.TryAdd(requestKey, tcs);
        var updatedPost = _mapper.Map<Post>(postRequestDto);

        var message = new KafkaMessage
        {
            MessageType = MessageType.Update,
            Data = JsonConvert.SerializeObject(updatedPost)
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var kafkaMessage = await tcs.Task;

        if (kafkaMessage.Data == null)
            return null;
        
        if (kafkaMessage.ErrorOccured)
        {
            throw new KafkaException(kafkaMessage.ErrorMessage);
        }
        
        var post = JsonConvert.DeserializeObject<Post>(kafkaMessage.Data);
        
        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<PostResponseDto?> DeletePost(long id)
    {
        var requestKey = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<KafkaMessage>();
        _responseCompletionSources.TryAdd(requestKey, tcs);

        var message = new KafkaMessage
        {
            MessageType = MessageType.Delete,
            Data = JsonConvert.SerializeObject(id)
        };
        
        await _messageBus.PublishAsync(requestKey, message);

        var kafkaMessage = await tcs.Task;

        if (kafkaMessage.Data == null)
            return null;
        
        if (kafkaMessage.ErrorOccured)
        {
            throw new KafkaException(kafkaMessage.ErrorMessage);
        }
        
        var post = JsonConvert.DeserializeObject<Post>(kafkaMessage.Data);
        
        return _mapper.Map<PostResponseDto>(post);
    }
}