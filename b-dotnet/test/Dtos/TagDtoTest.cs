using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Dtos
{
  public class TagDtoTest
  {
    [Fact]
    public void ShouldCreateTagDto()
    {
      TagDto tagDto = new() { Name = "Working", Color = ColorEnum.Blue };
      var context = new ValidationContext(tagDto);
      Action act = () => Validator.ValidateObject(tagDto, context, true);
      act.Should().NotThrow();
    }

    [Fact]
    public void TagDtoShouldFailNameValidation()
    {
      TagDto tagDto = new() { Name = "Bad", Color = ColorEnum.Blue };
      var context = new ValidationContext(tagDto);
      Action act = () => Validator.ValidateObject(tagDto, context, true);
      act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("minimum length of '5'"));
    }

    [Fact]
    public void TagDtoShouldFailColorValidation()
    {
      TagDto tagDto = new() { Name = "Very good", Color = (ColorEnum)7 };
      var context = new ValidationContext(tagDto);
      Action act = () => Validator.ValidateObject(tagDto, context, true);
      act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("The field Color must be between 0 and 2"));
    }
  }
}
