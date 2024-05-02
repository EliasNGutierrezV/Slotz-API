using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slotz_API.Data;
using Slotz_API.Models;

namespace Slotz_API.Controllers
{
    [Route("Slotz/[controller]")]
    [ApiController]
    public class SpaceGaragesController : Controller
    {
        private readonly dbContext _dbContext;

        public SpaceGaragesController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: SpaceGarages
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _dbContext.spacegarage != null ?
                        Ok(await _dbContext.spacegarage.ToListAsync()) :
                        Problem("Entity set 'dbContext.Vehicle'  is null.");
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]SpaceGarage newSpace)
        {
            if (newSpace == null)
                return BadRequest("Invalid vehicle data.");

            try
            {
                _dbContext.spacegarage.Add(newSpace);
                await _dbContext.SaveChangesAsync();
                return Ok(newSpace);
            }
            catch (Exception ex)
            {
                return Problem($"Error inserting vehicle: {ex.Message}");
            }
        }

        [HttpPatch("UpdateSpaceGarage")]
        [Authorize]
        public async Task<IActionResult> UpdateSpaceGarage([FromBody]SpaceGarage space)
        {

            var founded = await _dbContext.spacegarage.FindAsync(space.IdSpaceGarage);

            founded.Height = space.Height;
            founded.Width = space.Width;
            founded.Long = space.Long;
            founded.Price = space.Price;
            founded.Status = space.Status;
            founded.IdGarage = space.IdGarage;

            try
            {
                _dbContext.Entry(founded).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Error Updating vehicle: {ex.Message}");
            }

            return Ok(space);
        }

        [HttpDelete("DeleteSpaceGarage")]
        [Authorize]
        public async Task<IActionResult> DeleteSpaceGarage([FromBody]SpaceGarage space)
        {

            var founded = await _dbContext.spacegarage.FindAsync(space.IdSpaceGarage);

            try
            {
                _dbContext.Entry(founded).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Error Deleting vehicle: {ex.Message}");
            }
            return NoContent();
        }
    }
}
