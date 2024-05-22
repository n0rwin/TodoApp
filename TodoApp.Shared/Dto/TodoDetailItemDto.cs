namespace TodoApp.Shared.Dto;

public record TodoDetailItemDto(int Id, string Name, bool IsDone, DateTime? DueAt) : IdDto(Id);