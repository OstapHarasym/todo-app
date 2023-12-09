using SQLite;

namespace Lpnu.Cad.ToDoApp.Shared.DataTransferObjects;

public class TodoDto
{
    public TodoDto() {}

    public TodoDto(Guid id, string title, bool isCompleted)
    {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }

    public TodoDto(string title, bool isCompleted)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = isCompleted;
    }

    [PrimaryKey]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}