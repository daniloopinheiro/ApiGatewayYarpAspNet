using Microsoft.AspNetCore.Mvc;

namespace PrimeiroServico.Controllers;

[ApiController]
[Route("[controller]")]
public class PrimeiroController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<PrimeiroController> _logger;

    public PrimeiroController(ILogger<PrimeiroController> logger)
    {
        _logger = logger;
    }

    // [HttpGet]
    public IEnumerable<Primeiro> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Primeiro
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
