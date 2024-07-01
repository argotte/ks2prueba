using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(int pageNumber, int pageSize);
        Task<User> GetUser(int id);
        // Task<User> CreateUser(UserDto user);
        Task<User>? UpdateUser(int id, UserDto user);
        Task<bool>? DeleteUser(int id);
        
        Task<User?> Login(string email, string password);
        Task<bool> Register(User user, string password);
        string GenerateJwtToken(User user);
    }
}