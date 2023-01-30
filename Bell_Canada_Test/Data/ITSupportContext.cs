using Bell_Canada_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Bell_Canada_Test.Data
{
    public class ITSupportContext : DbContext
    {
        public ITSupportContext(DbContextOptions<ITSupportContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
