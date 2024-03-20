using System.ComponentModel.DataAnnotations;

namespace Forum.PostApi.Models.Dto;

public class PostRequestDto
{
    public int Guid { get; set; }

    [Length(2, 2048, ErrorMessage = "Wrong content length.")]
    public string Content { get; set; }
    
    public Guid StoryId { get; set; }
}