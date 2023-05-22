using Customers.DTO;
using Customers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        void Add(CreateCustomerDTO entity);
        CustomerDTO Update(CustomerDTO entity);
        void Delete(int id);
        CustomerDTO Get(int id);

    }
}
