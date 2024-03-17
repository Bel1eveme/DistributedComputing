using System.ComponentModel.DataAnnotations;

namespace Forum.Api.Models;

public class Creator
{
    public long Id { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}