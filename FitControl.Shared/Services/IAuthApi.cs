using FitControl.Shared.Models;
using Refit;
namespace FitControl.Shared.Services;

public interface IAuthApi
{
    [Get("/users")]
    Task<ApiResponse<List<User>>> GetUsers();
    
    [Post("/user")]
    Task<ApiResponse<User>> AddUser([Body] User user);
    
    [Get("/user")]
    Task<ApiResponse<bool>> GetUserAsync([Body] User user);
    
    [Put("/user")]
    Task<ApiResponse<User>> UpdateUser([Body] User user);

    [Delete("/user/{id}")]
    Task<ApiResponse<string>> DeleteUser(int id);
}