using Forum.PostApi.Kafka.Messages;
using Forum.PostApi.Models;
using Newtonsoft.Json;

namespace Forum.PostApi.Kafka.Consumer;

public class PostKafkaHandler : IKafkaHandler<string, KafkaMessage>
{
    private readonly IKafkaMessageBus<string, KafkaMessage> _kafkaMessageBus;

    public PostKafkaHandler(IKafkaMessageBus<string, KafkaMessage> kafkaMessageBus)
    {
        _kafkaMessageBus = kafkaMessageBus;
    }

    public async Task HandleAsync(string key, KafkaMessage value)
    {
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Post>(value.Data)));

        if (value.MessageType == MessageType.GetAll)
        {
            IEnumerable<Post> posts = Enumerable.Range(1, 10) // Генерация чисел от 1 до 10
                .Select(id => new Post 
                { 
                    Id = id, 
                    Content = "SecondApi" 
                });

            var message = new KafkaMessage
            {
                MessageType = MessageType.GetAll,
                Data = JsonConvert.SerializeObject(posts)
            };
            
            await _kafkaMessageBus.PublishAsync(key, message);
        }
        else
        {
            throw new NotImplementedException();
        }
        
        
    }
}