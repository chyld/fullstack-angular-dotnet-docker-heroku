using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
  public class TodoDto
  {
    [Range(0, 2)]
    public PriorityEnum Priority { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(10)]
    public string Title { get; set; }

    [Range(typeof(DateTime), "1/1/2021", "1/1/2023")]
    public DateTime Due { get; set; }
  }
}
