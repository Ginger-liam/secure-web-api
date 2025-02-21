using Microsoft.AspNetCore.Mvc;
using Module3WebApi.WebApi.Models;
using Module3WebApi.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Module3WebApi.WebApi.Controllers;

[ApiController]
[Route("Object2D")]
public class Object2DController : ControllerBase
{
    private readonly Object2DRepository _object2DRepository;
    private readonly ILogger<Object2DController> _logger;

    public Object2DController(Object2DRepository object2DRepository, ILogger<Object2DController> logger)
    {
        _object2DRepository = object2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadAllObjects2D")]
    public async Task<ActionResult<IEnumerable<Object2D>>> Get()
    {
        var objects = await _object2DRepository.ReadAllAsync();
        return Ok(objects);
    }

    [HttpGet("{object2DId}", Name = "ReadObject2D")]
    public async Task<ActionResult<Object2D>> Get(Guid object2DId)
    {
        var object2D = await _object2DRepository.ReadAsync(object2DId);
        if (object2D == null)
            return NotFound();

        return Ok(object2D);
    }

    [HttpPost(Name = "CreateObject2D")]
    public async Task<ActionResult> Add(Object2D object2D)
    {
        object2D.Id = Guid.NewGuid();
        await _object2DRepository.InsertAsync(object2D);
        return CreatedAtRoute("ReadObject2D", new { object2DId = object2D.Id }, object2D);
    }

    [HttpPut("{object2DId}", Name = "UpdateObject2D")]
    public async Task<ActionResult> Update(Guid object2DId, Object2D newObject2D)
    {
        var existingObject2D = await _object2DRepository.ReadAsync(object2DId);

        if (existingObject2D == null)
            return NotFound();

        existingObject2D.EnvironmentId = newObject2D.EnvironmentId;
        existingObject2D.PrefabId = newObject2D.PrefabId;
        existingObject2D.PositionX = newObject2D.PositionX;
        existingObject2D.PositionY = newObject2D.PositionY;
        existingObject2D.ScaleX = newObject2D.ScaleX;
        existingObject2D.ScaleY = newObject2D.ScaleY;
        existingObject2D.RotationZ = newObject2D.RotationZ;
        existingObject2D.SortingLayer = newObject2D.SortingLayer;

        await _object2DRepository.UpdateAsync(existingObject2D);

        return Ok(existingObject2D);
    }

    [HttpDelete("{object2DId}", Name = "DeleteObject2D")]
    public async Task<IActionResult> Delete(Guid object2DId)
    {
        var existingObject2D = await _object2DRepository.ReadAsync(object2DId);

        if (existingObject2D == null)
            return NotFound();

        await _object2DRepository.DeleteAsync(object2DId);

        return Ok();
    }
}
