using Microsoft.EntityFrameworkCore;
using Vivacity.Models;

namespace Vivacity.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Employee> Employees {get; set;} = default!; 
    public DbSet<Plant> Plants {get; set;} = default!;
    public DbSet<Machine> Machines {get; set;} = default!;
}