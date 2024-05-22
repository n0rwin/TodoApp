using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Interfaces;
using TodoApp.Model.Entities;
using TodoApp.Shared.Dto;

namespace TodoApp.Api.Controllers;

public abstract class AController<TController, TEntity, TListDto, TDetailDto, TAddDto> 
    : ControllerBase 
    where TEntity : IdEntity
    where TListDto : IdDto
{
    protected readonly ILogger<TController> Logger;
    protected readonly IRepository<TEntity> Repository;

    protected AController(ILogger<TController> logger, IRepository<TEntity> repository)
    {
        Logger = logger;
        Repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<TListDto>> Get(int start = 0, int count = 10)
    {
        Logger.LogTrace($"Called {nameof(Get)}");
        return await Repository.GetAsync<TListDto>(start, count);
    }

    [HttpGet("{id:int}")]
    public async Task<TDetailDto?> GetById(int id)
    {
        Logger.LogTrace($"Called {nameof(GetById)}({id})");
        return await Repository.GetByIdAsync<TDetailDto>(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<TListDto>> Post(TAddDto dto)
    {
        Logger.LogTrace($"Called {nameof(Post)}");
        var addedEntry = await Repository.AddAsync<TAddDto, TListDto>(dto);
        return CreatedAtAction(
            nameof(GetById), 
            ControllerName, 
            new { id = addedEntry.Id }, 
            addedEntry);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        Logger.LogTrace($"Called {nameof(Delete)}({id})");
        var wasDeleted = await Repository.DeleteAsync(id);

        return wasDeleted
            ? Ok()
            : NotFound();
    }

    private string ControllerName => 
        RouteData.Values["controller"]!.ToString()!;
}