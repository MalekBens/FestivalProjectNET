﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FestivalAdminDashboard.Models;
using MySqlConnector;

namespace FestivalAdminDashboard.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
// [Route("Home/Index/{id:int}")]
//  public IActionResult Index(int id)
//     {
//          using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
//         connection.Open();
           
            
       
//            using var commandDelete = new MySqlCommand("Delete from Event where EventID='"+id+"';", connection);
           
           
//         commandDelete.ExecuteNonQuery();
//         return RedirectToRoute("/home/index/1");
//     }
//     [Route("Home/Index/{id:int}")]
    public IActionResult Index( )
    {
        
        List<EventModel> listEvent = new List<EventModel> {};
        EventModel ev = new EventModel();
        try
        {
            using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
            connection.Open();
        using var command1 = new MySqlCommand("SELECT * FROM EVENT;", connection);
             using var reader = command1.ExecuteReader();
        while(reader.Read()){
            ev.eventID = reader.GetInt16(0);
            ev.eventName = reader.GetString(1);
            ev.eventDesc = reader.GetString(2);
            
            ev.presenterName = reader.GetString(3);
            
            ev.category = reader.GetString(4);
            
            ev.location = reader.GetString(5);
        listEvent.Add(new EventModel()  {eventID = ev.eventID, eventName = ev.eventName, eventDesc = ev.eventDesc, presenterName = ev.presenterName, category = ev.category, location = ev.location});
        }
        
        }
        catch (System.Exception)
        {
            
            Console.WriteLine("SS1");
          
        }
       @ViewData["Message"] = listEvent;
       
        
        return View();
    }
[Route("Home/Me/{id:int}")]
    public IActionResult Me(int id){
        Console.WriteLine( id);
         using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
        connection.Open();
           
            
       
           using var commandDelete = new MySqlCommand("Delete from Event where EventID='"+id+"';", connection);
           
           
        commandDelete.ExecuteNonQuery();
        return RedirectToAction("Index");
    }


[Route("Home/Edit/{id:int}")]
    public IActionResult Edit(int id){
        EventModel ev = new EventModel();
        Console.Write(id);
         using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
        connection.Open();
            using var commandSelectElement = new MySqlCommand("Select * from Event where eventID = '"+id+"';", connection);
             using var reader = commandSelectElement.ExecuteReader();
        while(reader.Read()){
             ev.eventName = reader.GetString(1);
            ev.eventDesc = reader.GetString(2);
            
            ev.presenterName = reader.GetString(3);
            
            ev.category = reader.GetString(4);
            
            ev.location = reader.GetString(5);
        }
        @ViewData["Message"] = ev.eventName;
        return View();
    }
public IActionResult editEvent(EventModel e){
using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=festival");
        connection.Open();
        using var commandDelete = new MySqlCommand("Update Event set EventDesc = '"+e.eventDesc+"', Presenter = '"+e.presenterName+"', Category = '"+e.category+"', Location = '"+e.location+"';", connection);
        commandDelete.ExecuteNonQuery();
return RedirectToAction("Index");
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}