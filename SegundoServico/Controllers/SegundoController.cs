<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;

namespace SegundoServico.Controllers;

[ApiController]
[Route("[controller]")]
public class SegundoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<SegundoController> _logger;

    public SegundoController(ILogger<SegundoController> logger)
    {
        _logger = logger;
    }
    
    // [HttpGet]
    public IEnumerable<Segundo> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Segundo
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
=======
using Microsoft.AspNetCore.Mvc;

namespace SegundoServico.Controllers;

[ApiController]
[Route("[controller]")]
public class SegundoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<SegundoController> _logger;

    public SegundoController(ILogger<SegundoController> logger)
    {
        _logger = logger;
    }
    
    // [HttpGet]
    public IEnumerable<Segundo> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Segundo
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
>>>>>>> refs/remotes/origin/main
