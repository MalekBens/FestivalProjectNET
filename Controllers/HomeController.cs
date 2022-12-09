using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using carthage.Models;
using carthage.Helper;


using carthage.DAL;

namespace carthage.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly ApplicationDbContext _context;
  private readonly JwtAuthenticationManager jwtAuthenticationManager;

  public HomeController(ILogger<HomeController> logger,
  ApplicationDbContext context,
  JwtAuthenticationManager jwtAuthenticationManager)
  {
    _logger = logger;
    _context = context;
    this.jwtAuthenticationManager = jwtAuthenticationManager;
  }

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
    List<Plan>? plans = _context.Plans.ToList();
    @ViewData["plans"] = plans;
    return View();
  }
  //   public IActionResult Index()
  //   {

  //     List<EventModel> listEvent = new List<EventModel> { };
  //     EventModel ev = new EventModel();
  //     try
  //     {
  //       using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
  //       connection.Open();
  //       using var command1 = new MySqlCommand("SELECT * FROM EVENT;", connection);
  //       using var reader = command1.ExecuteReader();
  //       while (reader.Read())
  //       {
  //         ev.eventID = reader.GetInt16(0);
  //         ev.eventName = reader.GetString(1);
  //         ev.eventDesc = reader.GetString(2);

  //         ev.presenterName = reader.GetString(3);

  //         ev.category = reader.GetString(4);

  //         ev.location = reader.GetString(5);
  //         listEvent.Add(new EventModel() { eventID = ev.eventID, eventName = ev.eventName, eventDesc = ev.eventDesc, presenterName = ev.presenterName, category = ev.category, location = ev.location });
  //       }

  //     }
  //     catch (System.Exception)
  //     {

  //       Console.WriteLine("SS1");

  //     }
  //     @ViewData["Message"] = listEvent;


  //     return View();
  //   }


  // [Route("Home/Me/{id:int}")]
  // public IActionResult Me(int id)
  // {
  //   Console.WriteLine(id);
  //   using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
  //   connection.Open();



  //   using var commandDelete = new MySqlCommand("Delete from Event where EventID='" + id + "';", connection);


  //   commandDelete.ExecuteNonQuery();
  //   return RedirectToAction("Index");
  // }


  // [Route("Home/Edit/{id:int}")]
  // public IActionResult Edit(int id)
  // {
  //   EventModel ev = new EventModel();
  //   Console.Write(id);
  //   using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
  //   connection.Open();
  //   using var commandSelectElement = new MySqlCommand("Select * from Event where eventID = '" + id + "';", connection);
  //   using var reader = commandSelectElement.ExecuteReader();
  //   while (reader.Read())
  //   {
  //     ev.eventName = reader.GetString(1);
  //     ev.eventDesc = reader.GetString(2);

  //     ev.presenterName = reader.GetString(3);

  //     ev.category = reader.GetString(4);

  //     ev.location = reader.GetString(5);
  //   }
  //   @ViewData["Message"] = ev.eventName;
  //   return View();
  // }
  // public IActionResult editEvent(EventModel e)
  // {
  //   using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
  //   connection.Open();
  //   using var commandDelete = new MySqlCommand("Update Event set EventDesc = '" + e.eventDesc + "', Presenter = '" + e.presenterName + "', Category = '" + e.category + "', Location = '" + e.location + "';", connection);
  //   commandDelete.ExecuteNonQuery();
  //   return RedirectToAction("Index");
  // }
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
