using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using carthage.DAL;
using carthage.Helper;
using carthage.Models;

namespace carthage.Controllers;

public class EventsController : Controller
{
  private readonly ILogger<EventsController> _logger;
  private readonly ApplicationDbContext _context;
  private readonly JwtAuthenticationManager jwtAuthenticationManager;

  public EventsController(ILogger<EventsController> logger,
  JwtAuthenticationManager jwtAuthenticationManager,
  ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
    this.jwtAuthenticationManager = jwtAuthenticationManager;
  }

  [Route("Events")]
  public IActionResult Index()
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null)
      {
        @ViewData["user"] = user;
      }
    }
    List<Event>? eventList = _context.Events.ToList();
    @ViewData["data"] = eventList;
    return View();
  }
  [Route("ticketEvent/{id:int}")]
  public IActionResult ticketEvent(int id)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null)
      {
        @ViewData["user"] = user;
      }
    }
    Event? ev = _context.Events.Where(e => e.ID == id).FirstOrDefault();
    List<Plan>? plans = _context.Plans.ToList();
    @ViewData["event"] = ev;
    @ViewData["plans"] = plans;
    return View();
  }

  [Route("buyTiket/{eventID:int}/{planID:int}")]
  public IActionResult buyTiket(int eventID, int planID)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null)
      {
        Event? ev = _context.Events.Where(e => e.ID == eventID).FirstOrDefault();
        Plan? planSelected = _context.Plans.Where(p => p.ID == planID).FirstOrDefault();
        if (ev != null && planSelected != null)
        {
          _context.Orders.Add(new Order
          {
            eventID = eventID,
            planID = planID,
            userID = user.ID,
            price = planSelected.price
          });
          _context.SaveChanges();
        }
        @ViewData["event"] = ev;
        List<Plan>? plans = _context.Plans.ToList();
        @ViewData["plans"] = plans;
        @ViewData["user"] = user;
        return Redirect("/ticketEvent/"+eventID);
      }
    }
    return RedirectToAction("signin", "auth");
  }
}