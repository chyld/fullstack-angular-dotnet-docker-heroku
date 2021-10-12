using System;
using Xunit;
using FluentAssertions;
using lib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using web;
using System.Threading.Tasks;

namespace test.Repositories
{
  public class TodoRepositoryTest
  {
    private Database _db;
    private ITodoRepository _repo;

    public TodoRepositoryTest()
    {
      var conn = new SqliteConnection("DataSource=:memory:");
      conn.Open();
      var options = new DbContextOptionsBuilder<Database>().UseSqlite(conn).Options;
      _db = new Database(options);
      _db.Database.EnsureDeleted();
      _db.Database.EnsureCreated();
      _repo = new TodoRepository(_db);
    }

    [Fact]
    public async Task ShouldSaveTodoToDatabase()
    {
      Todo todo = new Todo();
      await _repo.AddAsync(todo);
      await _repo.SaveAsync();
      _db.Todos.Count().Should().Be(1);
    }

    [Fact]
    public void ShouldToggleTodo()
    {
      Todo todo = new Todo();
      _repo.Toggle(todo);
      todo.IsOpen.Should().BeTrue();
    }

    [Fact]
    public void ShouldAddComment()
    {
      Todo todo = new Todo();
      Comment comment = new Comment();
      _repo.AddComment(todo, comment);
      todo.Comments.Should().HaveCount(1);
    }

    [Fact]
    public async Task ShouldGetAllTodos()
    {
      Todo todo1 = new Todo();
      Todo todo2 = new Todo();
      await _repo.AddAsync(todo1);
      await _repo.AddAsync(todo2);
      await _repo.SaveAsync();
      var todos = await _repo.GetAllAsync();
      todos.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldGetTodoById()
    {
      Todo todo1 = new Todo() { Id = Guid.Parse("f104f46e-fcc6-4e57-bb06-435944e33e6b") };
      Todo todo2 = new Todo() { Id = Guid.Parse("2e88ec5c-2b73-43ac-b500-bf0d9b33f3f6") };
      await _repo.AddAsync(todo1);
      await _repo.AddAsync(todo2);
      await _repo.SaveAsync();
      var todo = await _repo.GetByIdAsync(Guid.Parse("2e88ec5c-2b73-43ac-b500-bf0d9b33f3f6"));
      todo.Id.ToString().Should().Be("2e88ec5c-2b73-43ac-b500-bf0d9b33f3f6");
    }

    [Fact]
    public async Task ShouldAddNewTag()
    {
      Todo todo = new Todo() { Id = Guid.Parse("f104f46e-fcc6-4e57-bb06-435944e33e6b") };
      Tag tag = new Tag() { Name = "Home" };
      await _repo.AddTagAsync(todo, tag);
      await _repo.AddAsync(todo);
      await _repo.SaveAsync();
      var dbTodo = await _repo.GetByIdAsync(Guid.Parse("f104f46e-fcc6-4e57-bb06-435944e33e6b"));
      dbTodo.Tags.Should().HaveCount(1);
    }

    [Fact]
    public async Task ShouldNotAddDuplicateTags()
    {
      Todo todo = new Todo() { Id = Guid.Parse("f104f46e-fcc6-4e57-bb06-435944e33e6b") };
      Tag tag1 = new Tag() { Name = "Home" };
      Tag tag2 = new Tag() { Name = "Home" };
      await _repo.AddTagAsync(todo, tag1);
      await _repo.AddTagAsync(todo, tag2);
      await _repo.AddAsync(todo);
      await _repo.SaveAsync();
      var dbTodo = await _repo.GetByIdAsync(Guid.Parse("f104f46e-fcc6-4e57-bb06-435944e33e6b"));
      dbTodo.Tags.Should().HaveCount(1);
    }
  }
}
