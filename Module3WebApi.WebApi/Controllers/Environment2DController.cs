using Microsoft.AspNetCore.Mvc;
using Module3WebApi.WebApi.Models;
using Module3WebApi.WebApi.Repositories;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Module3WebApi.WebApi.Controllers;

[ApiController]
[Route("Environtment2D")]
public class Environment2DController : ControllerBase
{
    private readonly Environment2DRepository _environment2DRepository;
    private readonly ILogger<Environment2DController> _logger;

    public Environment2DController(Environment2DRepository environment2DRepository, ILogger<Environment2DController> logger)
    {
        _environment2DRepository = environment2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "Read2DEnvironment")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        var weatherForecasts = await _environment2DRepository.ReadAsync();
        return Ok(weatherForecasts);
    }

    //[HttpGet("{weatherForecastId}", Name = "ReadWeatherForecast")]
    //public async Task<ActionResult<WeatherForecast>> Get(Guid weatherForecastId)
    //{
    //    var weatherForeCast = await _environment2DRepository.ReadAsync(weatherForecastId);
    //    if (weatherForeCast == null)
    //        return NotFound();

    //    return Ok(weatherForeCast);
    //}

    //[HttpPost(Name = "CreateWeatherForecast")]
    //public async Task<ActionResult> Add(WeatherForecast weatherForecast)
    //{
       
    //    weatherForecast.Id = Guid.NewGuid();

    //    var createdWeatherForecast = await _environment2DRepository.InsertAsync(weatherForecast);
    //    return Created();
    //}

    //[HttpPut("{weatherForecastId}", Name = "UpdateWeatherForecast")]
    //public async Task<ActionResult> Update(Guid weatherForecastId, WeatherForecast newWeatherForeCast)
    //{
    //    var existingWeatherForecast = await _environment2DRepository.ReadAsync(weatherForecastId);

    //    if (existingWeatherForecast == null)
    //        return NotFound();

    //    await _environment2DRepository.UpdateAsync(newWeatherForeCast);

    //    return Ok(newWeatherForeCast);
    //}

    //[HttpDelete("{weatherForecastId}", Name = "DeleteWeatherForecastByDate")]
    //public async Task<IActionResult> Update(Guid weatherForecastId)
    //{
    //    var existingWeatherForecast = await _environment2DRepository.ReadAsync(weatherForecastId);

    //    if (existingWeatherForecast == null)
    //        return NotFound();

    //    await _environment2DRepository.DeleteAsync(weatherForecastId);

    //    return Ok();
    //}

}
