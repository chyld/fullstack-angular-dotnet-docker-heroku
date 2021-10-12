using System.Text.Json.Serialization;

namespace web
{
  public class Comment
  {
    public int Id { get; set; }
    public string Text { get; set; }
    [JsonIgnore]
    public Todo Todo { get; set; }
  }
}
