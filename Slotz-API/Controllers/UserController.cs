using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Slotz_API.Data;
using Slotz_API.Models;
using Slotz_API.Repositories.Interfaces;

namespace Slotz_API.Controllers
{
    [Route("Slotz/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly dbContext _dbContext;
        private readonly IAccess _access;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public UserController(dbContext dbContext, IAccess access, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _access = access;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            return _dbContext.user != null ?
                        Ok(await _dbContext.user.ToListAsync()) :
                        Problem("Entity set 'dbContext.User'  is null.");
        }

        [HttpPost("Create")]
        // POST: Slotz/User
        public async Task<IActionResult> Create([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                if(_access.Exists(newUser))
                    return Problem("The user is already taken");

                _dbContext.user.Add(newUser);
                await _dbContext.SaveChangesAsync();
                return Ok(newUser);

            }
            catch (Exception ex)
            {
                return Problem($"Error inserting user: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<IEnumerable<User>>> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                return BadRequest("Deben proporcionarse tanto Username como Password.");
            
            RequestModel session = _access.Authentication(loginRequest);

            if (session == null)
                return NotFound("No se encontraron usuarios con los criterios de búsqueda proporcionados.");

            // Guardar el usuario en la sesión
            string userJson = JsonConvert.SerializeObject(session);
            _httpContextAccessor.HttpContext.Session.SetString("CurrentUser", userJson);

            return Ok(userJson);
        }
    }
}
