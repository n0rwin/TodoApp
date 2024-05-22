using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Interfaces;
using TodoApp.Model.Entities;
using TodoApp.Shared.Dto;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("todos")]
public class TodoController(ILogger<TodoController> logger, IRepository<Todo> repository)
    : AController<TodoController, Todo, TodoListItemDto, TodoDetailItemDto, CreateTodoDto>(logger, repository);