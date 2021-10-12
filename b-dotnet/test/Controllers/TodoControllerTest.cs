using System;
using Xunit;
using FluentAssertions;
using web;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace test.Controllers
{
  public class TodoControllerTest
  {
    private TodoController _controller;
    private Mock<ITodoRepository> _mockRepository;
    private DefaultHttpContext _httpContext;

    public TodoControllerTest()
    {
      _mockRepository = new Mock<ITodoRepository>();
      _httpContext = new DefaultHttpContext();
      _controller = new TodoController(_mockRepository.Object)
      { ControllerContext = new ControllerContext() { HttpContext = _httpContext } };
    }

    [Fact]
    public async Task ShouldCreateTodo()
    {
      var dto = new TodoDto() { Priority = PriorityEnum.Medium, Title = "Clean House", Due = DateTime.Parse("01/01/2000") };
      var result = await _controller.Create(dto);
      var createdActionResult = result as CreatedAtActionResult;
      createdActionResult.StatusCode.Should().Be(201);
      createdActionResult.ActionName.Should().Be("GetOne");
      createdActionResult.RouteValues["Id"].Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldGetOneTodo()
    {
      Guid guid = Guid.NewGuid();
      _mockRepository.Setup(obj => obj.GetByIdAsync(guid)).Returns(Task.FromResult(new Todo() { Id = guid, Title = "Write Code" }));
      var result = await _controller.GetOne(guid);
      var okResult = result as OkObjectResult;
      okResult.StatusCode.Should().Be(200);
      (okResult.Value as Todo).Title.Should().Be("Write Code");
      _mockRepository.Verify(obj => obj.GetByIdAsync(guid));
    }

    [Fact]
    public async Task ShouldGetAllTodos()
    {
      var result = await _controller.GetAll();
      var okResult = result as OkObjectResult;
      okResult.StatusCode.Should().Be(200);
      _mockRepository.Verify(obj => obj.GetAllAsync());
    }

    [Fact]
    public async Task ShouldNotToggleTodoStateIncorrectId()
    {
      var result = await _controller.Toggle(Guid.NewGuid());
      var badResult = result as BadRequestResult;
      badResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ShouldToggleTodoState()
    {
      Guid guid = Guid.NewGuid();
      _mockRepository.Setup(obj => obj.GetByIdAsync(guid)).Returns(Task.FromResult(new Todo() { Id = guid, Title = "Write Code" }));
      var result = await _controller.Toggle(guid);
      var redirectActionResult = result as RedirectToActionResult;
      _mockRepository.Verify(obj => obj.Toggle(It.IsAny<Todo>()));
      redirectActionResult.RouteValues["Id"].Should().Be(guid);
      redirectActionResult.ActionName.Should().Be("GetOne");
    }

    [Fact]
    public async Task ShouldNotAddCommentBadId()
    {
      var result = await _controller.AddComment(Guid.NewGuid(), new CommentDto());
      var badResult = result as BadRequestResult;
      badResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ShouldAddComment()
    {
      Guid guid = Guid.NewGuid();
      _mockRepository.Setup(obj => obj.GetByIdAsync(guid)).Returns(Task.FromResult(new Todo() { Id = guid, Title = "Write Code" }));
      var result = await _controller.AddComment(guid, new CommentDto() { Text = "This is a comment" });
      var createdActionResult = result as CreatedAtActionResult;
      createdActionResult.StatusCode.Should().Be(201);
      createdActionResult.ActionName.Should().Be("GetOne");
      createdActionResult.RouteValues["Id"].Should().Be(guid);
      _mockRepository.Verify(obj => obj.AddComment(It.IsAny<Todo>(), It.IsAny<Comment>()));
    }

    [Fact]
    public async Task ShouldNotAddTagBadId()
    {
      var result = await _controller.AddTag(Guid.NewGuid(), new TagDto());
      var badResult = result as BadRequestResult;
      badResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ShouldAddTag()
    {
      Guid guid = Guid.NewGuid();
      _mockRepository.Setup(obj => obj.GetByIdAsync(guid)).Returns(Task.FromResult(new Todo() { Id = guid, Title = "Write Code" }));
      var result = await _controller.AddTag(guid, new TagDto());
      var createdActionResult = result as CreatedAtActionResult;
      createdActionResult.StatusCode.Should().Be(201);
      createdActionResult.ActionName.Should().Be("GetOne");
      createdActionResult.RouteValues["Id"].Should().Be(guid);
      _mockRepository.Verify(obj => obj.AddTagAsync(It.IsAny<Todo>(), It.IsAny<Tag>()));
    }
  }
}
