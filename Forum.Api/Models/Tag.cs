using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models;

public class Tag
{
    public long Id { get; set; }
    
    public string Text { get; set; }
    
    public List<Story> Issues { get; set; }
}