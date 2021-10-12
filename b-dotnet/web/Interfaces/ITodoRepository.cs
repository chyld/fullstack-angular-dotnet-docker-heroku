using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace web
{
  public interface ITodoRepository
  {
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo> GetByIdAsync(Guid id);
    void AddComment(Todo todo, Comment comment);
    Task AddTagAsync(Todo todo, Tag tag);
    void Toggle(Todo todo);
    Task AddAsync(Todo todo);
    Task SaveAsync();
  }
}
