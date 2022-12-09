namespace carthage.Models;

using System.ComponentModel.DataAnnotations;

public class Plan
{
  [Key]
  [Required]
  public int ID { get; set; }

  public string name { get; set; } = string.Empty;
  public string description { get; set; } = string.Empty;
  public int price { get; set; } = 0;

}
