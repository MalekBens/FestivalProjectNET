using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using carthage.Models;
using MySqlConnector;

namespace carthage.Controllers;


public class AdminController : Controller
{
  public static char ishow;
  private readonly ILogger<AdminController> _logger;

  public AdminController(ILogger<AdminController> logger)
  {
    _logger = logger;
  }

  [Route("Admin/AddEvent")]
  public IActionResult Index()
  {
    @ViewData["Message"] = ishow;
    @ViewData["role"] = "admin";
    return View("/Views/Admin/addevent.cshtml");
  }

  [Route("Admin/EventsList")]
  public IActionResult EventsList()
  {
    
    return View();
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
