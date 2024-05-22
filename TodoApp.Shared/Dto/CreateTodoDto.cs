namespace TodoApp.Shared.Dto;

public record CreateTodoDto(string Name, bool IsDone, DateTime? DueAt);