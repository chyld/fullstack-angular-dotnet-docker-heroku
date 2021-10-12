using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
  public class CommentDto
  {
    [Required]
    [MinLength(10)]
    [MaxLength(100)]
    public string Text { get; set; }
  }
}
