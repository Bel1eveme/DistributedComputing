using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models;

public class Post
{
    public long Id { get; set; }
    
    public string Content { get; set; }
    
    public Story Story { get; set; }
}