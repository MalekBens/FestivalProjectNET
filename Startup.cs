namespace carthage.startup;

using Microsoft.EntityFrameworkCore;
// using MySQL.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using carthage.DAL;
using carthage.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public class Startup
{
  public IConfiguration _configuration
  {
    get;
  }
  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllersWithViews();
    services.AddDbContext<ApplicationDbContext>(opt =>
    {
      Console.WriteLine("Connecting to Database");
      opt.UseMySQL(_configuration.GetConnectionString("DefaultConnection") ?? "");
    });
    services.AddDistributedMemoryCache();

    services.AddMemoryCache();
    services.AddSession();

    services.AddDatabaseDeveloperPageExceptionFilter();
    services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Token"])),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    services.AddSingleton<JwtAuthenticationManager>();
  }

}