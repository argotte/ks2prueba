using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        // public async Task<User> CreateUser(UserDto userDto)
        // {

        //     var user = new User
        //     {
        //         Name = userDto.Name,
        //         Email = userDto.Email,
        //         Password = userDto.Password,
        //         CreatedAt = DateTime.Now,
        //         UpdatedAt = DateTime.Now
        //     };
        //     var createdUser = await _context.Users.AddAsync(user);
        //     await _context.SaveChangesAsync();
        //     return createdUser.Entity;
        // }

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

        public async Task<List<User>> GetUsers(int pageNumber, int pageSize)
        {
            var users = await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return users;
        }

        public async Task<User>? UpdateUser(int id, UserDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            existingUser.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingUser;
            
        }


public async Task<User?> Login(string email, string password)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
    {
        return null;
    }

    return user;
}

public async Task<bool> Register(User user, string password)
{
    try
    {
        var mailAddress = new System.Net.Mail.MailAddress(user.Email);
    }
    catch (FormatException)
    {
        return false;
    }

    if (await _context.Users.AnyAsync(u => u.Email == user.Email))
    {
        return false;
    }

    user.Password = BCrypt.Net.BCrypt.HashPassword(password);

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return true;
}

public string GenerateJwtToken(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("miclavededieciseisdigitosesesta"); // Replace with your secret key
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
}
    }
}