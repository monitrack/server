using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : ApiControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok();
    }
}