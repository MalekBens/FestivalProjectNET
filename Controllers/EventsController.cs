using Microsoft.AspNetCore.Mvc;

namespace carthage.Controllers;

public class EventsController : Controller
{
  private readonly ILogger<EventsController> _logger;

  public EventsController(ILogger<EventsController> logger)
  {
    _logger = logger;
  }

  [Route("Events")]
  public IActionResult Index()
  {
    return View();
  }
  [Route("ticketEvent/{$id}")]
  public IActionResult ticketEvent(int id)
  {
    return View();
  }
}