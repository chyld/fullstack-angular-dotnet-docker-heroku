using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
  public class TagDto
  {
    [Required]
    [MinLength(5)]
    [MaxLength(10)]
    public string Name { get; set; }

    [Range(0, 2)]
    public ColorEnum Color { get; set; }
  }
}
