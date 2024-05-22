namespace TodoApp.Shared.Dto;

public record TodoListItemDto(int Id, string Name) : IdDto(Id);