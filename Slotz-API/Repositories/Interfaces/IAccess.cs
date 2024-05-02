using Microsoft.AspNetCore.Identity.Data;
using Slotz_API.Models;

namespace Slotz_API.Repositories.Interfaces
{
    public interface IAccess
    {
        public RequestModel Authentication(LoginRequest loginRequest);
        public bool Exists(User user);
    }
}
