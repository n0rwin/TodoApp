using AutoMapper;
using TodoApp.Model.Entities;
using TodoApp.Shared.Dto;

namespace TodoApp.Domain.Mapping;

public class TodoMapperProfile : Profile
{
    public TodoMapperProfile()
    {
        CreateMap<Todo, TodoListItemDto>();
        CreateMap<Todo, TodoDetailItemDto>();
        CreateMap<CreateTodoDto, Todo>();
    }
}