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
  public DbSet<Event> Events { get; set; }
  public DbSet<Plan> Plans { get; set; }
  public DbSet<Order> Orders { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Order>().ToTable("orders")
    .HasOne<Event>(e => e.ev).WithMany().HasForeignKey(u => u.eventID);
    modelBuilder.Entity<Order>().ToTable("orders")
    .HasOne<Plan>(e => e.plan).WithMany().HasForeignKey(u => u.planID);
    modelBuilder.Entity<Order>().ToTable("orders")
    .HasOne<User>(e => e.user).WithMany().HasForeignKey(u => u.userID);

    modelBuilder.Entity<Event>().ToTable("Events").Ignore(e => e.image);

    modelBuilder.Entity<Plan>().ToTable("Plans");

    modelBuilder.Entity<User>().ToTable("Users")
    .HasOne<Role>(u => u.role).WithMany().HasForeignKey(u => u.roleID).IsRequired();

    modelBuilder.Entity<Role>().ToTable("Roles").HasMany<User>()
    .WithOne(u => u.role).HasForeignKey(u => u.roleID).OnDelete(DeleteBehavior.Cascade);
  }

}