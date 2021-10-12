using System;
using Xunit;
using FluentAssertions;
using web;

namespace test.Models
{
  public class TodoTest
  {
    [Fact]
    public void ShouldCreateTodo()
    {
      Todo todo = new Todo();
      todo.Tags.Should().HaveCount(0);
      todo.Comments.Should().HaveCount(0);
    }

    [Fact]
    public void ShouldCreateTodoFromDto()
    {
      TodoDto dto = new TodoDto() { Priority = PriorityEnum.Medium, Title = "Do Laundry", Due = DateTime.Now };

      Todo todo = new Todo(dto);
      todo.Tags.Should().HaveCount(0);
      todo.Comments.Should().HaveCount(0);
      todo.IsOpen.Should().BeTrue();
      todo.Priority.Should().Be(PriorityEnum.Medium);
      todo.Title.Should().Be("Do Laundry");
      todo.Due.Should().BeOnOrBefore(DateTime.Now);
    }
  }
}
