using Slotz_API.Models;

namespace Slotz_API.Repositories.Interfaces
{
    public interface IVehicleService
    {
        public Vehicle VehicleExists(Vehicle vehicle);
    }
}
