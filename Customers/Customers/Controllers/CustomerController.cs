using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Customers.Context;
using Customers.Models;
using Microsoft.EntityFrameworkCore;
using Customers.Repositories;
using Customers.Services;
using Customers.DTO;
using System.Reflection.Metadata.Ecma335;

namespace Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
     
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAll();
            if (customers.Any())
                return Ok(customers);
            else
                return NoContent();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var desiredCustomer = _customerService.Get(id);
                
            if (desiredCustomer != null)
                return Ok(desiredCustomer);
            else
                return NotFound();
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerDTO customer)
        {
            if (customer == null)
                return BadRequest();

          

          
            try
            {
                _customerService.Add(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody]CustomerDTO customer)
        {
            if (customer == null)
                return BadRequest();

            var updatedCustomer = _customerService.Update(customer);

            return Ok(updatedCustomer);

            
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {

            try
            {
                _customerService.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
            return NoContent();
        }
    }

}
    

