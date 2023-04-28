using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Customers.Context;
using Customers.Models;
using Microsoft.EntityFrameworkCore;


namespace Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _dataContext.Customers.ToList();
            if (customers.Any())
                return Ok(customers);
            else
                return NoContent();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var desiredCustomer = _dataContext.Customers.
                Where(c => c.Id == id)
                .FirstOrDefault();

            if (desiredCustomer != null)
                return Ok(desiredCustomer);
            else
                return NotFound();
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            if (_dataContext.Customers.Where(c => c.Name.Equals(customer.Name)).FirstOrDefault() != null)
                return BadRequest($"Customer {customer.Name} already exist");

            if (customer.Gender != 'M' && customer.Gender != 'F')
                return BadRequest($"Customer {customer.Name} gender is unknown");

            _dataContext.Customers.Add(customer);
            _dataContext.SaveChanges();

            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            var desiredCustomer = _dataContext.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();

            if (desiredCustomer == null)
            {
                
                _dataContext.Customers.Add(customer);
                _dataContext.SaveChanges();
                return Ok(customer);
            }
            else
            {
                
                desiredCustomer.Name = customer.Name;
                desiredCustomer.Gender = customer.Gender;
                desiredCustomer.Address = customer.Address;
                desiredCustomer.Age = customer.Age;
                _dataContext.SaveChanges();
                return Ok(desiredCustomer);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var desiredCustomer = _dataContext.Customers.Where(c => c.Id == id).FirstOrDefault();

            if (desiredCustomer == null)
            {
                return NotFound($"No customer with id {id} exists.");
            }

            _dataContext.Customers.Remove(desiredCustomer);
            _dataContext.SaveChanges();

            return NoContent();
        }
    }
}
    

