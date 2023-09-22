using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RestfulWebAPI.Models
{
   public class ApplicationDbContext : DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {

      }
      public DbSet<Institute> Institute { get; set; }
      public DbSet<Branch> Branches { get; set; }
      public DbSet<Employee> Employees { get; set; }
   }
}
