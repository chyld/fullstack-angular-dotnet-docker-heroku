using Microsoft.EntityFrameworkCore;

namespace web
{
  public class Database : DbContext
  {
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public Database(DbContextOptions<Database> options) : base(options) { }
  }
}
