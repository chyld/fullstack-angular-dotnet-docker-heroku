using System;
using Xunit;
using FluentAssertions;
using web;

namespace test.Models
{
  public class CommentTest
  {
    [Fact]
    public void ShouldCreateComment()
    {
      Comment comment = new Comment() { Id = 1, Text = "My comment" };
      comment.Id.Should().Be(1);
      comment.Text.Should().Be("My comment");
      comment.Todo.Should().BeNull();
    }
  }
}
