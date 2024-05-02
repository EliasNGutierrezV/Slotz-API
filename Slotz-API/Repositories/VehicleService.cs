using Microsoft.EntityFrameworkCore;
using Slotz_API.Data;
using Slotz_API.Models;
using Slotz_API.Repositories.Interfaces;

namespace Slotz_API.Repositories
{
    public class VehicleService : IVehicleService
    {
        private readonly dbContext _dbContext;
        public VehicleService(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  Vehicle VehicleExists(Vehicle vehicle)
        {
            if (vehicle == null) throw new ArgumentNullException();

            var findPlate = _dbContext.vehicle.Find(vehicle.IdVehicle);

            if(findPlate == null) return null;

            return findPlate;
        }
    }
}
