using LionStrategiesTest.DTOs.Users;
using LionStrategiesTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionStrategiesTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto createUserDto)
        {
            try
            {
                var newUser = await _userService.CreateAsync(createUserDto);
                return CreatedAtAction(nameof(Create), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                
                return Conflict(new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return StatusCode(403); // Forbidden
            }

            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return StatusCode(403); 
            }

            var success = await _userService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}