using Microsoft.AspNetCore.Mvc;
using Module3WebApi.WebApi.Models;
using Module3WebApi.WebApi.Repositories;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Module3WebApi.WebApi.Controllers;

[ApiController]
[Route("Environment2D")]
public class Environment2DController : ControllerBase
{
    private readonly Environment2DRepository _environment2DRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<Environment2DController> _logger;

    public Environment2DController(Environment2DRepository environment2DRepository, ILogger<Environment2DController> logger, IAuthenticationService authenticationService)
    {
        _environment2DRepository = environment2DRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    [HttpGet(Name = "Read2DEnvironment")]
    public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
    {
        var environments = await _environment2DRepository.ReadAsync();
        return Ok(environments);
    }

    [HttpGet("{environment2DId}", Name = "ReadEnvironment2D")]
    public async Task<ActionResult<Environment2D>> Get(Guid environment2DId)
    {
        Console.WriteLine(_authenticationService.GetCurrentAuthenticatedUserId());
        var environment2D = await _environment2DRepository.ReadAsync(environment2DId);
        if (environment2D == null)
            return NotFound();

        return Ok(environment2D);
    }

    [HttpPost(Name = "CreateEnvironment2D")]
    public async Task<ActionResult> Add(Environment2D environment2D)
    {

        environment2D.Id = Guid.NewGuid();

        var createdWeatherForecast = await _environment2DRepository.InsertAsync(environment2D);
        return Created();
    }

    [HttpPut("{environment2DId}", Name = "UpdateEnvironment2D")]
    public async Task<ActionResult> Update(Guid environment2DId, Environment2D newEnvironment2D)
    {
        var existingEnvironment2D = await _environment2DRepository.ReadAsync(environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        // Update only the fields that changed
        if (newEnvironment2D.Name != null && existingEnvironment2D.Name != newEnvironment2D.Name)
            existingEnvironment2D.Name = newEnvironment2D.Name;

        if (newEnvironment2D.OwnerUserId != null && existingEnvironment2D.OwnerUserId != newEnvironment2D.OwnerUserId)
            existingEnvironment2D.OwnerUserId = newEnvironment2D.OwnerUserId;

        if (newEnvironment2D.MaxLength != 0 && existingEnvironment2D.MaxLength != newEnvironment2D.MaxLength)
            existingEnvironment2D.MaxLength = newEnvironment2D.MaxLength;

        if (newEnvironment2D.MaxHeight != 0 && existingEnvironment2D.MaxHeight != newEnvironment2D.MaxHeight)
            existingEnvironment2D.MaxHeight = newEnvironment2D.MaxHeight;

        await _environment2DRepository.UpdateAsync(existingEnvironment2D);

        return Ok(existingEnvironment2D);
    }

    [HttpDelete("{environment2DId}", Name = "DeleteEnvironment2D")]
    public async Task<IActionResult> Delete(Guid environment2DId)
    {
        var existingEnvironment2D = await _environment2DRepository.ReadAsync(environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        await _environment2DRepository.DeleteAsync(environment2DId);

        return Ok();
    }

}
