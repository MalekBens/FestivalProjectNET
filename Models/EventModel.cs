namespace carthage.Models;

public class Event
{
  public int? ID { get; set; }
  public string? name { get; set; }
  public string? presenter { get; set; }

  public string? description { get; set; }

  public string? category { get; set; }

  public string? location { get; set; }
  public string? imagePath { get; set; }
  public IFormFile? image { get; set; }

}
