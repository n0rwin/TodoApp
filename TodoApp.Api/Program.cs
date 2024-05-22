using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Implementations;
using TodoApp.Domain.Interfaces;
using TodoApp.Domain.Mapping;
using TodoApp.Model.Configuration;
using TodoApp.Model.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(TodoMapperProfile));

builder.Services.AddDbContextFactory<TodoDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IRepository<Todo>, TodoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();