using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_and_Task_API.Data;
using User_and_Task_API.Models;

namespace User_and_Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ApiContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("GetUser failed: User {UserId} not found", id);
                return NotFound();
            }

            return user;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} created successfully", user.ID);
            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.ID)
                return BadRequest("User ID mismatch");

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User {UserId} updated successfully", user.ID);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.ID == id))
                {
                    _logger.LogWarning("Update failed: User {UserId} not found", id);
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Delete failed: User {UserId} not found", id);
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} deleted successfully", id);
            return NoContent();
        }
    }
}
