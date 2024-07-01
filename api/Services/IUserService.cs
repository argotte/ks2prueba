using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> CreateUser(UserDto user);
        Task<User>? UpdateUser(int id, UserDto user);
        Task<bool>? DeleteUser(int id);
        
    }
}