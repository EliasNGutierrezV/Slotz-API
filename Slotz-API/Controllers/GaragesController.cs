using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slotz_API.Data;
using Slotz_API.Models;
using Slotz_API.Repositories.Interfaces;
using System.Security.Claims;

namespace Slotz_API.Controllers
{
    [Route("Slotz/[controller]")]
    [ApiController]
    public class GaragesController : Controller
    {
        private readonly dbContext _dbContext;

        public GaragesController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Vehicles
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _dbContext.garage != null ?
                        Ok(await _dbContext.garage.ToListAsync()) :
                        Problem("Entity set 'dbContext.Vehicle'  is null.");
        }

        [HttpGet("{idUser}")]
        [Authorize]
        public async Task<IActionResult> GetGaragesByUserId(int idUser)
        {
            try
            {
                // Filtrar los garajes por el ID del usuario
                var garages = await _dbContext.garage.Where(g => g.IdUser == idUser&& g.Status==1).ToListAsync();

                return Ok(garages); // Devuelve la lista de garajes del usuario si existen
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message); // Devuelve un error genérico con el mensaje de excepción
            }
        }

        [HttpGet("GetGarage/{idGarage}")]
        [Authorize]
        public async Task<IActionResult> GetGaragesById(int idGarage)
        {
            try
            {
                // Filtrar los garajes por el ID del usuario
                var garages = await _dbContext.garage.Where(g => g.IdGarage == idGarage).ToListAsync();

                return Ok(garages); // Devuelve la lista de garajes del usuario si existen
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message); // Devuelve un error genérico con el mensaje de excepción
            }
        }

        [HttpGet("{idGarage}/{idUser}")]
        [Authorize]
        public async Task<IActionResult> GetGarageById(int idGarage, int idUser)
        {
            try
            {
                var garage = await _dbContext.garage.FindAsync(idGarage);

                if (garage == null)
                {
                    return NotFound(); // Devuelve un 404 Not Found si no se encuentra el garaje
                }

                // Verifica que el usuario propietario del garaje sea el mismo que se pasa como parámetro
                if (garage.IdUser != idUser)
                {
                    return Forbid(); // Devuelve un 403 Forbidden si el usuario no es el propietario del garaje
                }

                return Ok(garage); // Devuelve el objeto garaje si se encuentra y el usuario es propietario
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message); // Devuelve un error genérico con el mensaje de excepción
            }
        }
        /*[HttpGet("{idGarage}")]
        [Authorize]
        public async Task<IActionResult> GetGaragesById(int id)
        {
            try
            {
                // Filtrar los garajes por el ID del usuario
                var garages = await _dbContext.garage.Where(g => g.IdGarage == id).ToListAsync();

                return Ok(garages); // Devuelve la lista de garajes del usuario si existen
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message); // Devuelve un error genérico con el mensaje de excepción
            }
        }*/



        // GET: Vehicles/Details/5
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Garage newGarage)
        {
            if (newGarage == null)
                return BadRequest("Invalid vehicle data.");

            try
            {
                _dbContext.garage.Add(newGarage);
                await _dbContext.SaveChangesAsync();
                return Ok(newGarage);
            }
            catch (Exception ex)
            {
                return Problem($"Error inserting vehicle: {ex.Message}");
            }
        }

        [HttpPatch("UpdateGarage")]
        [Authorize]
        public async Task<IActionResult> UpdateGarage([FromBody] Garage garage)
        {

            var founded = await _dbContext.garage.FindAsync(garage.IdGarage);

            founded.Name = garage.Name;
            founded.Address = garage.Address;
            founded.Longitude = garage.Longitude;
            founded.Latitude = garage.Latitude;
            founded.Description = garage.Description;
            founded.IdUser = garage.IdUser;

            try
            {
                _dbContext.Entry(founded).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Error Updating vehicle: {ex.Message}");
            }

            return Ok(garage);
        }
        [HttpPatch("UpdateGarage/{idGarage}")]
        [Authorize]
        public async Task<IActionResult> UpdateGarage(int idGarage, [FromBody] Garage garage)
        {
            try
            {
                var founded = await _dbContext.garage.FindAsync(idGarage);

                // Verifica si el garaje encontrado existe y pertenece al usuario que está intentando actualizarlo
                if (founded == null || founded.IdUser != garage.IdUser)
                {
                    return Forbid(); // Devuelve un 403 Forbidden si el garaje no se encuentra o no pertenece al usuario
                }

                founded.Name = garage.Name;
                founded.Address = garage.Address;
                founded.Longitude = garage.Longitude;
                founded.Latitude = garage.Latitude;
                founded.Description = garage.Description;

                _dbContext.Entry(founded).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(garage);
            }
            catch (Exception ex)
            {
                return Problem($"Error Updating garage: {ex.Message}");
            }
        }


        [HttpDelete("DeleteGarage")]
        [Authorize]
        public async Task<IActionResult> DeleteGarage([FromBody] Garage garage)
        {

            var founded = await _dbContext.garage.FindAsync(garage.IdGarage);

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
        [HttpDelete("DeleteGarage/{idGarage}")]
        [Authorize]
        [HttpPatch("DeleteGarage/{idGarage}")]
        [Authorize]
        public async Task<IActionResult> DeleteGarage(int idGarage, [FromBody] Garage garage)
        {
            try
            {
                var founded = await _dbContext.garage.FindAsync(idGarage);

                if (founded == null)
                {
                    return NotFound(); // Devuelve un 404 Not Found si el garaje no se encuentra
                }


                founded.Status = 0;

                _dbContext.Entry(founded).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(garage);
            }
            catch (Exception ex)
            {
                return Problem($"Error Deleting garage: {ex.Message}");
            }
        }


    }
}
