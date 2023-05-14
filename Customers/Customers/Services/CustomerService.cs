using Customers.DTO;
using Customers.Models;
using Customers.Repositories;

namespace Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private IRepositories<Customer> _repository;

        public CustomerService(IRepositories <Customer> repository) 
        {
            _repository = repository;
        }
        public void Add(CustomerDTO customerDto)
        {
            var customerHasSameName = _repository.GetAll()
                .Where(c => c.Name.Equals(customerDto.Name))
                .FirstOrDefault() != null;

            if (customerHasSameName)
                throw new Exception($"Customer: {customerDto.Name} already exist.");

            var customerModel = new Customer    
            {
                Name = customerDto.Name,
                Gender = customerDto.Gender,
                Address = customerDto.Address,
                Age = customerDto.Age
            };

            _repository.Add(customerModel);
        }

        public void Delete(CustomerDTO customerDto)
        {
            var desiredCustomer = _repository.Get(customerDto.Id);

            if (desiredCustomer == null)
                throw new Exception($"No customerDto with id {customerDto.Id} exists.");

            _repository.Delete(desiredCustomer);
        }

        public CustomerDTO Get(int id)
        {
            var customerModel = _repository.Get(id);
            return new CustomerDTO
            {
                Id = customerModel.Id,
                Name = customerModel.Name,
                Age = customerModel.Age,
                Address = customerModel.Address,
                Gender = customerModel.Gender,
            };
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var customers = _repository.GetAll();
			return customers.Select(c => new CustomerDTO
			{
				Id = c.Id,
				Name = c.Name,
				Gender = c.Gender,
				Age = c.Age,
				Address = c.Address,
			});
        }

        public CustomerDTO Update(CustomerDTO customerDTO, CustomerDTO updatedCustomerDTO)
        {
            var desiredCustomer = _repository.Get(customerDTO.Id);

            if (desiredCustomer == null)
            {
                _repository.Add(new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    Age = customerDTO.Age,
                    Address = customerDTO.Address,
                    Gender = customerDTO.Gender,
                });
                return customerDTO;
            }
            else
            {
                _repository.Update(desiredCustomer, new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    Age = customerDTO.Age,
                    Address = customerDTO.Address,
                    Gender = customerDTO.Gender,
                });
                return new CustomerDTO
                {
                    Id = desiredCustomer.Id,
                    Name = desiredCustomer.Name,
                    Age = customerDTO.Age,
                    Address = desiredCustomer.Address,
                    Gender = desiredCustomer.Gender,
                };
            }
        }
    }
}
