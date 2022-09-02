using Microsoft.AspNetCore.Mvc;

namespace TSB.Portal.Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InsuranceController : ControllerBase
{
   
    public InsuranceController()
    {
    }

    [HttpGet(Name = "GetInsurance")]
    public ObjectResult GetInsurance()
    {
        return new ObjectResult("");
    }
}
