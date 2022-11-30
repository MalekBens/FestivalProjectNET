namespace carthage.startup;

using Microsoft.EntityFrameworkCore;
// using MySQL.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using carthage.DAL;

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
      opt.UseMySQL(_configuration.GetConnectionString("DefaultConnection")??"");

    });

    services.AddDatabaseDeveloperPageExceptionFilter();
  }

}