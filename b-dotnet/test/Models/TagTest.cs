using System;
using Xunit;
using FluentAssertions;
using web;

namespace test.Models
{
  public class TagTest
  {
    [Fact]
    public void ShouldCreateTag()
    {
      Tag tag = new Tag() { Name = "Home", Color = ColorEnum.Blue };
      tag.Name.Should().Be("Home");
      tag.Color.Should().Be(ColorEnum.Blue);
      tag.Todos.Should().HaveCount(0);
    }
  }
}
