using Confluent.Kafka;

namespace Forum.Messaging.Producer;

public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
{
    public string Topic { get; set; }
}