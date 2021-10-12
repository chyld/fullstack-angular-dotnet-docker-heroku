using System;
using Xunit;
using FluentAssertions;
using web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace test.Dtos
{
  public class TodoDtoTest
  {
    [Fact]
    public void ShouldCreateTodoDto()
    {
      TodoDto todoDto = new() { Priority = PriorityEnum.Medium, Title = "Do Work", Due = DateTime.Parse("1/1/2022") };
      var context = new ValidationContext(todoDto);
      Action act = () => Validator.ValidateObject(todoDto, context, true);
      act.Should().NotThrow();
    }

    [Fact]
    public void TodoDtoShouldFailTitleValidation()
    {
      TodoDto todoDto = new() { Priority = PriorityEnum.Medium, Title = "No", Due = DateTime.Parse("1/1/2022") };
      var context = new ValidationContext(todoDto);
      Action act = () => Validator.ValidateObject(todoDto, context, true);
      act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("minimum length of '3'"));
    }

    [Fact]
    public void TodoDtoShouldFailPriorityValidation()
    {
      TodoDto todoDto = new() { Priority = (PriorityEnum)100, Title = "Do Work", Due = DateTime.Parse("1/1/2022") };
      var context = new ValidationContext(todoDto);
      Action act = () => Validator.ValidateObject(todoDto, context, true);
      act.Should().Throw<ValidationException>().Where(e => e.Message.Contains("0 and 2"));
    }

    [Fact]
    public void TodoDtoShouldFailDueValidation()
    {
      TodoDto todoDto = new() { Priority = PriorityEnum.Medium, Title = "Do Work", Due = DateTime.Parse("1/1/2020") };
      var context = new ValidationContext(todoDto);
      Action act = () => Validator.ValidateObject(todoDto, context, true);
      act.Should().Throw<ValidationException>()
        .Where(e => e.Message.Contains("The field Due must be between 1/1/2021 12:00:00 AM and 1/1/2023 12:00:00 AM."));
    }
  }
}
