using AutoMapper;
using TodoApp.Model.Configuration;
using TodoApp.Model.Entities;

namespace TodoApp.Domain.Implementations;

public class TodoRepository : ARepository<Todo>
{
    public TodoRepository(TodoDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }
}