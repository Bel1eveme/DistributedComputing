using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models.Dto;

public class TagRequestDto
{
    public int Id { get; set; }

    [Length(2, 32, ErrorMessage = "Wrong length.")]
    public string Text { get; set; } = string.Empty;
    
    public List<StoryRequestDto> Issues { get; set; }
}