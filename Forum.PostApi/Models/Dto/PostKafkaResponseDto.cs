namespace Forum.Api.Models.Dto;

public class PostKafkaResponseDto
{
    public bool ErrorOccured { get; set; } = false;

    public string ErrorMessage { get; set; } = string.Empty;

    public string? Country { get; set; }
    
    public long StoryId { get; set; }
    
    public long Id { get; set; }
    
    public string Content { get; set; }
}