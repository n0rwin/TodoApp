namespace TodoApp.Model.Entities;

public class Todo : IdEntity
{
    public string Name { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueAt { get; set; }
}