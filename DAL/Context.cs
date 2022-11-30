using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
// using System.Data.Entity;
// using System.Data.Entity.ModelConfiguration.Conventions;

namespace carthage.DAL;
using carthage.Models;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  public DbSet<User>? Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().ToTable("Users");
  }

  // protected override void OnModelCreating(DbModelBuilder modelBuilder)
  // {
  //   modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
  // }

}