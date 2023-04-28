using Microsoft.EntityFrameworkCore;
using Customers.Models;

namespace Customers.Context
{
    public class DataContext: DbContext
    {
        public DbSet<Customer> Customers
        {
            get; set;
        }
        public DataContext(DbContextOptions options): base (options)
        {
            
        }
    }

}
