using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using carthage.Models.Auth;
using carthage.Models;
using carthage.DAL;
using carthage.Helper;

namespace carthage.Controllers;

public class AuthController : Controller
{
  public static char ishow;
  private readonly ILogger<AuthController> _logger;
  private readonly ApplicationDbContext _context;
  private readonly JwtAuthenticationManager jwtAuthenticationManager;
  public AuthController(ILogger<AuthController> logger, ApplicationDbContext context, JwtAuthenticationManager jwtAuthenticationManager)
  {
    _logger = logger;
    _context = context;
    this.jwtAuthenticationManager = jwtAuthenticationManager;
  }

  [Route("Auth/signin")]
  public IActionResult SignIn()
  {
    return View("/Views/Auth/signin.cshtml");
  }


  [Route("Auth/signup")]
  public IActionResult SignUp()
  {
    return View("/Views/Auth/signup.cshtml");
  }

  [Route("Auth/signinHandle")]
  public IActionResult SignInHandle(Signin data)
  {
    string password = Crypte.EncodePasswordToBase64(data.password);
    User? user = _context.Users.Where(user => user.email == data.email && user.password == password).FirstOrDefault();
    if (user != null)
    {
      var token = jwtAuthenticationManager.Authenticate(data.email);
      HttpContext.Session.SetString("token", token);
      return RedirectToAction("index", "home");
    }
    else
    {
      @TempData["error"] = "Check your credentials";
      return RedirectToAction("signin");
    }
  }

  [Route("Auth/SignUpHandle")]
  public IActionResult SignUpHandle(SignUp data)
  {
    string password = Crypte.EncodePasswordToBase64(data.password);
    User? user = _context.Users.Where(user => user.email == data.email).FirstOrDefault();
    if (user != null)
    {
      @TempData["error"] = "Email olready exist";
      return RedirectToAction("signup");
    }
    else
    {
      _context.Users.Add(new User()
      {
        email = data.email,
        password = password,
        firstName = data.firstName,
        lastName = data.lastName
      });
      _context.SaveChanges();
      return RedirectToAction("signup");
    }
  }


}