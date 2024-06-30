using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services
{

    public class UserService : IUserService
    {
            private static List<User> users = new List<User>
        {
            new User
            {
            Id = 3, Name = "User 1", Email = "asd@gmail.com",Password = "123456",
                CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now,
            },
        };
        public async Task<User> CreateUser(User user)
        {
            users.Add(user);
            return user;
        }

        public Task<bool>? DeleteUser(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;    
            }
            users.Remove(user);
            return Task.FromResult(true);
        }
        public Task<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;           
            }
                return Task.FromResult(user);
            }

        public Task<List<User>> GetUsers()
        {
            return Task.FromResult(users);
        }

        public Task<User>? UpdateUser(int id, User user)
        {
            var existingUser = users.FirstOrDefault(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;
            return Task.FromResult(existingUser);}
    }
}