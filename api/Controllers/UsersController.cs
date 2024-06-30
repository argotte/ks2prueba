using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User
            {
            Id = 3, Name = "User 1", Email = "asd@gmail.com",Password = "123456",
                CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now,
            },
        };
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // return Ok("GetUsers");
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = users.FirstOrDefault(x => x.Id == id);
            if (existingUser == null)
            {
                return NotFound($"User with id {id} not found");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }
            users.Remove(user);
            return Ok("sUer deleted successfully");
        }

    }
}