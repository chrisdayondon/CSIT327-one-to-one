using Customers.Context;
using Customers.Models;

namespace Customers.Repositories
{
    public class CustomerRepository : IRepositories<Customer>
    {
        private CustomerContext _context;
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
            
        }
        public void Add(Customer customer)
        {

            _context.Customers.Add(customer);
            _context.SaveChanges();


        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public Customer Get(int id)
        {
            return _context.Customers.
               Where(c => c.Id == id)
               .FirstOrDefault();
        }

        public IEnumerable<Customer> GetAll()
        {
            
            return _context.Customers.ToList(); 
         
        }

        public void Update(Customer customer, Customer updatedCustomer)
        {
            customer.Name = updatedCustomer.Name;
            customer.Gender = updatedCustomer.Gender;
            customer.Address = updatedCustomer.Address;
            customer.Age = updatedCustomer.Age;
      
            
            _context.SaveChanges();
        }
    }
}
