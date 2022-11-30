using Microsoft.AspNetCore.Mvc;

namespace carthage.Controllers;

public class TicketsController : Controller
{
  private readonly ILogger<TicketsController> _logger;

  public TicketsController(ILogger<TicketsController> logger)
  {
    _logger = logger;
  }

  [Route("Tickets")]
  public IActionResult Index()
  {
    return View();
  }
}