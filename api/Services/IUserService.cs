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
        Task<User> CreateUser(User user);
        Task<User>? UpdateUser(int id, User user);
        Task<bool>? DeleteUser(int id);
        
    }
}