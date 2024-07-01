using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   [Authorize]
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
        public async Task<ActionResult<List<User>>> GetUsers([FromQuery] int pageNumber=1, [FromQuery] int pageSize=10)
        {
            var users = await _userService.GetUsers(pageNumber, pageSize);
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
        // [HttpPost]
        // public async Task<ActionResult<List<User>>> CreateUser([FromBody] UserDto user)
        // {
        //     var result = await _userService.CreateUser(user);
        //     return Ok(result);
        // }

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
        [AllowAnonymous]
        [HttpPost("RegisterJWT")]
        public async Task<IActionResult> RegisterJWT([FromBody] UserDto userDto)
        {
            // Validación de entrada
            if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Email and password are required");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                var result = await _userService.Register(user, userDto.Password);

                if (!result)
                {
                    return BadRequest("Could not register user");
                }

                var token = _userService.GenerateJwtToken(user);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userDto)
        {
            // Validación de entrada
            if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Email and password are required");
            }

            try
            {
                var user = await _userService.Login(userDto.Email, userDto.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                var token = _userService.GenerateJwtToken(user);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}