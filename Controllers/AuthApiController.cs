using Microsoft.AspNetCore.Mvc;

namespace carthage.Controllers;

using System.Net;
using System.Net.Http;
using System.Web;


[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
  public static char ishow;
  private readonly ILogger<AuthController> _logger;

  public AuthApiController(ILogger<AuthController> logger)
  {
    _logger = logger;
  }


  [HttpPost]
  [Route("signin")]
  public HttpResponseMessage signin()
  {
    Console.WriteLine("test");
    return new HttpResponseMessage(HttpStatusCode.BadRequest);
  }


}