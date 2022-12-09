namespace carthage.Models;

using System.ComponentModel.DataAnnotations;

public class Order
{
  [Key]
  [Required]
  public int ID { get; set; }

  public int? eventID { get; set; }
  public Event ev { get; set; }
  public int? planID { get; set; }
  public Plan plan { get; set; }
  public int? userID { get; set; }
  public User user { get; set; }
  public int price { get; set; } = 0;

}
