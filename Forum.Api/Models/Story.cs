using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models;

public class Story
{
    public long Id { get; set; }
    
    public string Title { get; set; }
    
    
    public string Content { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified  { get; set; }

    public Creator Creator { get; set; }
    
    public List<Tag> Tags { get; set; }
}