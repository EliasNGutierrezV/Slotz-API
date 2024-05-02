using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slotz_API.Data;
using Slotz_API.Models;
using Slotz_API.Repositories.Interfaces;

namespace Slotz_API.Controllers
{
    [Route("Slotz/[controller]")]
    [ApiController]
    public class VehiclesController : Controller
    {
        private readonly dbContext _dbContext;
        private readonly IVehicleService _vehicleService;

        public VehiclesController(dbContext dbContext, IVehicleService vehicleService)
        {
            _dbContext = dbContext;
            _vehicleService = vehicleService;
        }

        // GET: Vehicles
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _dbContext.vehicle != null ?
                        Ok(await _dbContext.vehicle.ToListAsync()) :
                        Problem("Entity set 'dbContext.Vehicle'  is null.");
        }

        // GET: Vehicles/Details/5
        [HttpPost("Create")]
        [Authorize]
        // POST: Slotz/Vehicle
        public async Task<IActionResult> Create([FromBody] Vehicle newVehicle)
        {
            if (newVehicle == null)
                return BadRequest("Invalid vehicle data.");

            try
            {
                _dbContext.vehicle.Add(newVehicle);
                await _dbContext.SaveChangesAsync();
                return Ok(newVehicle);
            }
            catch (Exception ex)
            {
                return Problem($"Error inserting vehicle: {ex.Message}");
            }
        }

        [HttpPatch("UpdateVehicle")]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle([FromBody]Vehicle vehicle)
        {

            Vehicle founded = _vehicleService.VehicleExists(vehicle);

            founded.Height = vehicle.Height;
            founded.Width = vehicle.Width;
            founded.Long = vehicle.Long;
            founded.Plate = vehicle.Plate;
            founded.UpdateDate = vehicle.UpdateDate;
            founded.Description = vehicle.Description;
            founded.IdUser = vehicle.IdUser;

            try
            {
                _dbContext.Entry(founded).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Problem($"Error Updating vehicle: {ex.Message}");
            }

            return Ok(vehicle);
        }

        [HttpDelete("DeleteVehicle")]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle([FromBody]Vehicle vehicle)
        {

            Vehicle founded = _vehicleService.VehicleExists(vehicle);

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
