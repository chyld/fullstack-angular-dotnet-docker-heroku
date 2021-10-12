using System;
using System.Collections.Generic;

namespace web
{
  public class Todo
  {
    public Guid Id { get; set; }
    public bool IsOpen { get; set; }
    public PriorityEnum Priority { get; set; }
    public string Title { get; set; }
    public DateTime Due { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }

    public Todo()
    {
      Tags = new();
      Comments = new();
    }

    public Todo(TodoDto todoDto)
    {
      Id = Guid.NewGuid();
      IsOpen = true;
      Priority = todoDto.Priority;
      Title = todoDto.Title;
      Due = todoDto.Due;
      Tags = new();
      Comments = new();
    }
  }
}
