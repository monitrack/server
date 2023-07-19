using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Server.Controllers;

namespace test;

public class ExpenseTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ExpenseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateExpenseTestsAsync()
    {
        var client = _factory.CreateClient();

        var content = new ExpenseCreateDto(100.0M);
        var response = await client.PostAsJsonAsync("/expense", content);

        response.EnsureSuccessStatusCode();
    }
}
