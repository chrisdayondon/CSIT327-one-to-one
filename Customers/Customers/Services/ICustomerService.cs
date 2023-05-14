using Customers.DTO;
using Customers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        void Add(CreateCustomerDTO entity);
        CustomerDTO Update(CustomerDTO entity, CustomerDTO updatedEntity);
        void Delete(CustomerDTO entity);
        CustomerDTO Get(int id);

    }
}
