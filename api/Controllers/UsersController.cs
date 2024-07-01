using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _userService;
        public UsersController (IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser([FromBody] UserDto user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, [FromBody] UserDto user)
        {
            var result = await _userService.UpdateUser(id, user);
            if (result == null)
            {
                return NotFound($"User with id {id} not found");
            }
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result == null)
            {
                return NotFound($"User with id {id} not found");
            }
            return Ok(result);
        }

    }
}