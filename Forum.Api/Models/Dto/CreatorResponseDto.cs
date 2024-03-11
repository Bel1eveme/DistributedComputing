namespace Forum.Api.Models.Dto;

public class CreatorResponseDto
{
    public long Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}