namespace carthage.Models;

using System.ComponentModel.DataAnnotations;

public class Role
{
  [Key]
  [Required]
  public int ID { get; set; }

  public string name { get; set; } = string.Empty;

}
