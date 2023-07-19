using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("expense")]
public class ExpenseController : ApiControllerBase
{
    [HttpPost]
    public IActionResult CreateExpense([FromBody] ExpenseCreateDto expense)
    {
        return Ok(expense);
    }
}

public record ExpenseCreateDto(decimal amount);
