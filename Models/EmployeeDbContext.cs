using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ofz1.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employees> Employees { get; set; }
    }
}
