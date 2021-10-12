using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace web
{
  public class Tag
  {
    [Key]
    public string Name { get; set; }
    public ColorEnum Color { get; set; }
    [JsonIgnore]
    public List<Todo> Todos { get; set; } = new();
  }
}
