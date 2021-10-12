using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Dtos
{
  public class CommentDtoTest
  {
    [Fact]
    public void ShouldCreateCommentDto()
    {
      CommentDto commentDto = new() { Text = "hello world" };
      var context = new ValidationContext(commentDto);
      Action act = () => Validator.ValidateObject(commentDto, context, true);
      act.Should().NotThrow();
    }

    [Fact]
    public void CommentDtoShouldFailValidaton()
    {
      CommentDto commentDto = new() { Text = "short" };
      var context = new ValidationContext(commentDto);
      Action act = () => Validator.ValidateObject(commentDto, context, true);
      act.Should().Throw<ValidationException>();
    }
  }
}
