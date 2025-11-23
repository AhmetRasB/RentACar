using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RentACarServer.Application.Services;
namespace RentACarServer.Infrastructure.Services
{
    internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public Guid GetUserId()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var claims = httpContext?.User.Claims;
            string UserId = claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId is null)
            {
                throw new ArgumentException("User Information Not Found");
            }

            try
            {
                Guid id = Guid.Parse(UserId);
                return id;


            }
            catch (Exception)
            {
               throw new ArgumentException("User id is not like guid format");
            }
        }
    }
}
