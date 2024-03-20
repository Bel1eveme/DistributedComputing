using Forum.PostApi.Models.Dto;

namespace Forum.PostApi.Services;

public interface IPostService
{
    public Task<List<PostResponseDto>> GetAllPosts();

    public Task<PostResponseDto?> GetPost(long id);
    
    public Task<PostResponseDto> CreatePost(PostRequestDto postRequestDto); 
    
    public Task<PostResponseDto?> UpdatePost(PostRequestDto postRequestDto); 
    
    public Task<PostResponseDto?> DeletePost(long id); 
}