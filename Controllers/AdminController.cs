using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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

  public async Task<IActionResult> addEventHandle(Event ev)
  {
    var fileName = Path.GetRandomFileName()+".jpeg";
    var filePath = Path.Combine("C:/Users/wadis/Desktop/dotnet/FestivalProjectNET/wwwroot/assets/img",
            fileName);

    using (var stream = System.IO.File.Create(filePath))
    {
      await ev.image.CopyToAsync(stream);
      ev.imagePath = fileName;
    }

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
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        List<Plan>? planList = _context.Plans.ToList();
        @ViewData["user"] = user;
        @ViewData["plans"] = planList;
        return View();
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/Plans/{id:int}")]
  public IActionResult EditPlansHandle(int id, EditPlan data)
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        Plan? planToEdit = _context.Plans.Where(p => p.ID == id).FirstOrDefault();
        if (planToEdit != null)
        {
          planToEdit.price = data.price;
          _context.SaveChanges();
        }
        List<Plan>? planList = _context.Plans.ToList();
        @ViewData["user"] = user;
        @ViewData["plans"] = planList;
        return View("/Views/Admin/EditPlans.cshtml");
      }
    }
    return RedirectToAction("index", "home");
  }

  [Route("Admin/OrdersList")]
  public IActionResult OrdersList()
  {
    var token = HttpContext.Session.GetString("token");
    if (token != null)
    {
      string? email = jwtAuthenticationManager.DecriptToken(token);
      User? user = _context.Users.Include(u => u.role).Where(u => u.email == email).FirstOrDefault();
      if (user != null && user.role.name == "ADMIN")
      {
        List<Order>? orders = _context.Orders.Include(u => u.ev).Include(u => u.plan).Include(u => u.user).ToList();
        @ViewData["user"] = user;
        @ViewData["orders"] = orders;
        return View("/Views/Admin/OrdersList.cshtml");
      }
    }
    return RedirectToAction("index", "home");
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
