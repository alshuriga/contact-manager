using ContactManagerTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerTest.Infrastructure
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }  

        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder modelConfigurationBuilder)
        {
            modelConfigurationBuilder.Properties<decimal>().HavePrecision(10, 2);
        }
    }
}
