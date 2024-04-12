using Forum.Messaging.Producer;

namespace Forum.Messaging.MessageBus;

public class KafkaMessageBus<Tk, Tv> : IMessageBus<Tk, Tv>
{
    public readonly KafkaProducer<Tk, Tv> Producer;
    public KafkaMessageBus(KafkaProducer<Tk, Tv> producer)
    {
        Producer = producer;
    }
    public async Task PublishAsync(Tk key, Tv message)
    {
        await Producer.ProduceAsync(key, message);
    }
}