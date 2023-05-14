using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Customers.Context;
using Customers.Models;
using Microsoft.EntityFrameworkCore;
using Customers.Repositories;

namespace Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IRepositories<Customer> _repository;

        public CustomerController(IRepositories<Customer> repository)
        {
            _repository = repository;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _repository.GetAll();
            if (customers.Any())
                return Ok(customers);
            else
                return NoContent();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var desiredCustomer = _repository.Get(id);
                
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

            if (_repository.GetAll().Where(c => c.Name.Equals(customer.Name)).FirstOrDefault() != null)
                return BadRequest($"Customer {customer.Name} already exist");

            if (customer.Gender != 'M' && customer.Gender != 'F')
                return BadRequest($"Customer {customer.Name} gender is unknown");

          _repository.Add(customer);

            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            var desiredCustomer = _repository.GetAll().Where(c => c.Id == customer.Id).FirstOrDefault();

            if (desiredCustomer == null)
            {
                _repository.Add(customer);
                return Ok(customer);
            }
            else
            {

                _repository.Update(desiredCustomer, customer);
               
                return Ok(desiredCustomer);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var desiredCustomer = _repository.GetAll().Where(c => c.Id == id).FirstOrDefault();

            if (desiredCustomer == null)
            {
                return NotFound($"No customer with id {id} exists.");
            }

            _repository.Delete(desiredCustomer);

            return NoContent();
        }
    }
}
    

