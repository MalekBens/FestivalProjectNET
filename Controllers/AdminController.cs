using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using carthage.DAL;
using carthage.Models;
using carthage.Helper;

namespace carthage.Controllers;


public class AdminController : Controller
{
  public static char ishow;
  private readonly ILogger<AdminController> _logger;
  private readonly ApplicationDbContext _context;
  private readonly JwtAuthenticationManager jwtAuthenticationManager;

  public AdminController(ILogger<AdminController> logger,
  ApplicationDbContext context,
  JwtAuthenticationManager jwtAuthenticationManager)
  {
    _logger = logger;
    _context = context;
    this.jwtAuthenticationManager = jwtAuthenticationManager;
  }

  [Route("Admin/AddEvent")]
  public IActionResult Index()
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        @ViewData["user"] = user;
        @ViewData["Message"] = "-1";
        return View("/Views/Admin/addevent.cshtml");
      }
    }
    return RedirectToAction("index", "home");
  }

  public IActionResult addEventHandle(Event ev)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        @ViewData["user"] = user;
        try
        {
          Event? eventAddeed = _context.Events.Add(ev).Entity;
          _context.SaveChanges();
          @ViewData["Message"] = "1";
          return View("/Views/Admin/addevent.cshtml");
        }
        catch (System.Exception)
        {
          @ViewData["Message"] = "0";
          return View("/Views/Admin/addevent.cshtml");
        }
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/EventsList")]
  public IActionResult EventsList()
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        @ViewData["user"] = user;
        List<Event>? eventList = _context.Events.ToList();
        @ViewData["data"] = eventList;
        return View();
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/Event/{id:int}")]
  public IActionResult editEvent(Event ev, int id)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        @ViewData["user"] = user;
        Event? eventToEdit = _context.Events.Where(e => e.ID == id).FirstOrDefault();
        if (eventToEdit != null)
        {
          eventToEdit.description = ev.description;
          eventToEdit.category = ev.category;
          eventToEdit.location = ev.location;
          eventToEdit.presenter = ev.presenter;
          _context.SaveChanges();
        }
        List<Event>? eventList = _context.Events.ToList();
        @ViewData["data"] = eventList;
        return View("/Views/Admin/eventsList.cshtml");
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/deleteEvent/{id:int}")]
  public IActionResult deleteEvent(int id)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        @ViewData["user"] = user;
        Event? eventTodelete = _context.Events.Where(e => e.ID == id).FirstOrDefault();
        if (eventTodelete != null)
        {
          _context.Remove(eventTodelete);
          _context.SaveChanges();
        }
        List<Event>? eventList = _context.Events.ToList();
        @ViewData["data"] = eventList;
        return View("/Views/Admin/eventsList.cshtml");
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/Plans")]
  public IActionResult EditPlans()
  {

    return View();
  }

  [Route("Admin/OrdersList")]
  public IActionResult OrdersList()
  {

    return View("/Views/Admin/OrdersList.cshtml");
  }

  // AddEvent/setEventDetails
  // public IActionResult setEventDetails(EventModel e)
  // {
  //   using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
  //   connection.Open();
  //   using var command = new MySqlCommand("INSERT INTO event  (EventName,EventDesc,Presenter,Category,Location)  VALUES('" + e.eventName + "','" + e.eventDesc + "','" + e.presenterName
  //   + "','" + e.category + "','" + e.location + "');", connection);

  //   try
  //   {
  //     command.ExecuteNonQuery();
  //     ishow = '1';
  //   }
  //   catch (System.Exception)
  //   {
  //     ishow = '0';
  //     return RedirectToAction("Index");
  //   }
  //   connection.Close();
  //   return RedirectToAction("Index");
  // }

  // public IActionResult dontshow()
  // {
  //   ishow = '2';
  //   return RedirectToAction("Index");
  // }
}
