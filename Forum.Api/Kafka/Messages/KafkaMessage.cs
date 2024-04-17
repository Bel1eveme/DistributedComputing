namespace Forum.Api.Kafka.Messages;

public class KafkaMessage
{
    public MessageType MessageType { get; set; }

    public string Data;
}