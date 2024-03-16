using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Images.Controllers;

[ApiController]
[Route("/api/images")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast"), Route("")]
    public String Get()
    {
        return "Hello";
    }
}
