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

  public DbSet<User> Users { get; set; }
  public DbSet<Role> Roles { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().ToTable("Users")
    .HasOne<Role>(u => u.role).WithMany().HasForeignKey(u => u.roleID).IsRequired();
    modelBuilder.Entity<Role>().ToTable("Roles").HasMany<User>()
    .WithOne(u => u.role).HasForeignKey(u => u.roleID).OnDelete(DeleteBehavior.Cascade);
  }

}