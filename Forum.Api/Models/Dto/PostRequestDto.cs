using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models.Dto;

public class PostRequestDto
{
    public int Id { get; set; }

    [Length(2, 2048, ErrorMessage = "Wrong content length.")]
    public string Content { get; set; }
    
    public Story Story { get; set; }
}