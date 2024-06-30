using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{

    public class UserService : IUserService
    {
        //     private static List<User> users = new List<User>
        // {
        //     new User
        //     {
        //     Id = 3, Name = "User 1", Email = "asd@gmail.com",Password = "123456",
        //         CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now,
        //     },
        // };
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            var createdUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return createdUser.Entity;
        }

        public async Task<bool>? DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return false;    
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;           
            }
                return user;
            }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User>? UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;
            return existingUser;
            }
    }
}