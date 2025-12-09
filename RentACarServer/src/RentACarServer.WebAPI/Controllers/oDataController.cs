using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace RentACarServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]
    public class ODataController : ControllerBase
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EnableLowerCamelCase();
            //builder.EntitySet<UserResponse>("employees");
            return builder.GetEdmModel();
        }
    }
}
