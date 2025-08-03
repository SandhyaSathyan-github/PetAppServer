using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PetAppServer.Model;

namespace PetAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create User
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("User with this email already exists.");

            user.Id = Guid.NewGuid();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Map to DTO
            var responseDto = new RegisterResDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserType = user.UserType
            };

            return Ok(responseDto);
        }

        // Login User
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
                return Unauthorized("Invalid credentials. Email not found.");

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (!isPasswordCorrect)
                return Unauthorized("Invalid credentials. Password not matching.");

            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType
            };

            return Ok(userResponse);
        }

        // Get User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType
            };

            return Ok(userResponse);
        }

        // Update Admin Details
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAdminDetails([FromBody] User admin)
        {
            var user = await _context.Users.FindAsync(admin.Id);
            if (user == null)
                return NotFound("Admin not found.");

            user.FirstName = admin.FirstName;
            user.LastName = admin.LastName;
            user.Email = admin.Email;
            user.Phone = admin.Phone;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // Change Password
        [HttpPut("change-password")]
        public async Task<IActionResult> UpdateAdminPassword([FromQuery] Guid userId, [FromQuery] string oldPassword, [FromQuery] string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                return BadRequest("Incorrect old password.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return Ok("Password changed successfully.");
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabaseConnection([FromServices] IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    return Ok("✅ Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Connection failed: {ex.Message}");
            }
        }
    }
}
