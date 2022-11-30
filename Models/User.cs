namespace carthage.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
  [Key]
  [Column("id")]
  public int ID { get; set; }
  [Required]
  public string firstName { get; set; } = string.Empty;
  [Required]
  public string lastName { get; set; } = string.Empty;
  [Required]
  public string email { get; set; } = string.Empty;
  [Required]
  public string password { get; set; } = string.Empty;

  [Display(Name = "fullName")]
  public string fullName
  {
    get
    {
      return firstName + " " + lastName;
    }
  }
}