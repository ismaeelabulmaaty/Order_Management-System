using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.HandlingErrors;

namespace Order_Management.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        public ActionResult Error(int Code)
        {
            return NotFound(new ApisResponse(Code));
        }
    }
}
