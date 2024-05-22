using Microsoft.EntityFrameworkCore;
using TodoApp.Model.Entities;

namespace TodoApp.Model.Configuration;

public class TodoDbContext(DbContextOptions<TodoDbContext> options)
    : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
}