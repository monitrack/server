using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("api/[controller]")]
public class IncomeController : ApiControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetIncomeInfoById(int id)
    {
        throw new NotImplementedException();
    }
}