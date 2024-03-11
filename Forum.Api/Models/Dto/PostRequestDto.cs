﻿namespace Forum.Api.Models.Dto;

public class PostRequestDto
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;
    
    public Story Story { get; set; }
}