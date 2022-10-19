using System.Data.Entity;

namespace WebApiCRUD.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}