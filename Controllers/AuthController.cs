using Microsoft.AspNetCore.Mvc;

namespace carthage.Controllers;

using System.Net;
using System.Net.Http;
using System.Web;


public class AuthController : Controller
{
  public static char ishow;
  private readonly ILogger<AuthController> _logger;

  public AuthController(ILogger<AuthController> logger)
  {
    _logger = logger;
  }

  [Route("Auth/signin")]
  public IActionResult SignIn()
  {
    return View("/Views/Auth/signin.cshtml");
  }

  [HttpPost]
  [ActionName("signin")]
  public HttpResponseMessage SignInApi()
  {
    return new HttpResponseMessage(HttpStatusCode.BadRequest);
  }


  [Route("Auth/signup")]
  public IActionResult SignUp()
  {
    return View("/Views/Auth/signup.cshtml");
  }


}