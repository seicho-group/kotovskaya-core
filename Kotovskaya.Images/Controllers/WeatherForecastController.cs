using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Images.Controllers;

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
