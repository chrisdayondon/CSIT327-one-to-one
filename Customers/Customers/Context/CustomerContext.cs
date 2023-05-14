using Microsoft.EntityFrameworkCore;
using Customers.Models;

namespace Customers.Context
{
    public class CustomerContext: DbContext
    {
        public DbSet<Customer> Customers
        {
            get; set;
        }
        public CustomerContext(DbContextOptions options): base (options)
        {
            
        }
    }

}
