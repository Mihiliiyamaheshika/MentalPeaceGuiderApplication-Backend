using Microsoft.AspNetCore.Mvc;

namespace MentalPeaceGuider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MentalPeaceGuiderController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Mental Peace Guider API is working!");
        }
    }
}
