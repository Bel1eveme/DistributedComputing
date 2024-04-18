using Forum.PostApi.Kafka.Messages;
using Forum.PostApi.Models;
using Forum.PostApi.Models.Dto;
using Forum.PostApi.Services;
using Newtonsoft.Json;

namespace Forum.PostApi.Kafka.Consumer;

public class PostKafkaHandler : IKafkaHandler<string, KafkaMessage>
{
    private readonly IKafkaMessageBus<string, KafkaMessage> _kafkaMessageBus;

    private readonly IPostService _postService;

    public PostKafkaHandler(IKafkaMessageBus<string, KafkaMessage> kafkaMessageBus, IPostService postService)
    {
        _kafkaMessageBus = kafkaMessageBus;
        _postService = postService;
    }

    public async Task HandleAsync(string key, KafkaMessage value)
    {
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Post>(value.Data)));
        KafkaMessage message = new KafkaMessage();

        switch (value.MessageType)
        {
            case MessageType.GetAll:
            {
                try
                {
                    var posts = _postService.GetAllPostsAsync();

                    message = new KafkaMessage
                    {
                        MessageType = MessageType.GetAll,
                        Data = JsonConvert.SerializeObject(posts)
                    };
                }
                catch (Exception e)
                {
                    message = new KafkaMessage
                    {
                        MessageType = MessageType.GetAll,
                        Data = null,
                        ErrorOccured = true,
                        ErrorMessage = e.Message,
                    };
                }

                break;
            }
            case MessageType.GetById:
            {
                try
                {
                    var postToFind = JsonConvert.DeserializeObject<int>(value.Data);

                    var post = _postService.GetPostAsync(postToFind);

                    message = new KafkaMessage
                    {
                        MessageType = MessageType.GetById,
                        Data = JsonConvert.SerializeObject(post)
                    };
                }
                catch (Exception e)
                {
                    message = new KafkaMessage
                    {
                        MessageType = MessageType.GetById,
                        Data = null,
                        ErrorOccured = true,
                        ErrorMessage = e.Message,
                    };
                }
                
                break;
            }
            case MessageType.Create:
                try
                {
                    var postToCreate = JsonConvert.DeserializeObject<Post>(value.Data);

                    var newPost = new PostRequestDto
                    {
                        Country = "ru",
                        Id = postToCreate.Id,
                        StoryId = postToCreate.StoryId,
                        Content = postToCreate.Content,
                    };
                    
                    var post = _postService.CreatePostAsync(newPost);

                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Create,
                        Data = JsonConvert.SerializeObject(post)
                    };
                }
                catch (Exception e)
                {
                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Create,
                        Data = null,
                        ErrorOccured = true,
                        ErrorMessage = e.Message,
                    };
                }
                
                break;
            case MessageType.Update:
                try
                {
                    var postToFind = JsonConvert.DeserializeObject<int>(value.Data);

                    var post = _postService.UpdatePostAsync(postToFind);

                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Update,
                        Data = JsonConvert.SerializeObject(post)
                    };
                }
                catch (Exception e)
                {
                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Update,
                        Data = null,
                        ErrorOccured = true,
                        ErrorMessage = e.Message,
                    };
                }
                
                break;
            case MessageType.Delete:
                try
                {
                    var postToFind = JsonConvert.DeserializeObject<int>(value.Data);

                    var post = _postService.DeletePostAsync(postToFind);

                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Delete,
                        Data = JsonConvert.SerializeObject(post)
                    };
                }
                catch (Exception e)
                {
                    message = new KafkaMessage
                    {
                        MessageType = MessageType.Delete,
                        Data = null,
                        ErrorOccured = true,
                        ErrorMessage = e.Message,
                    };
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await _kafkaMessageBus.PublishAsync(key, message);
    }
}