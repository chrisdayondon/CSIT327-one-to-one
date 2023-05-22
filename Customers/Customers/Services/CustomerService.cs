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
        public void Add(CreateCustomerDTO customerDto)
        {
            
               

                if (customerDto.Gender != 'M' && customerDto.Gender != 'F')
                    throw new Exception("invalid gender");
               

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
                Age = customerDto.Age,
                RewardCard = new RewardCard
                {
                    Points = customerDto.Points,
                    StoreIssued = customerDto.StoreIssued,
                }

                
            };

            _repository.Add(customerModel);
        }

        public void Delete(int id)
        {
            var desiredCustomer = _repository.Get(id);

            if (desiredCustomer == null)
                throw new Exception($"No customerDto with id {id} exists.");

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
                Points = customerModel.RewardCard.Points,
                StoreIssued = customerModel.RewardCard.StoreIssued,
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
                Points = c.RewardCard.Points,
                StoreIssued = c.RewardCard.StoreIssued,

			});
        }

        public CustomerDTO Update(CustomerDTO customerDTO)
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
                    RewardCard = new RewardCard
                    {
                        Points = customerDTO.Points,
                        StoreIssued= customerDTO.StoreIssued,
                    }
                    
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
                    RewardCard = new RewardCard
                    {
                        Points = customerDTO.Points,
                        StoreIssued = customerDTO.StoreIssued,
                    }
                    
                    
                });
                return new CustomerDTO
                {
                    Id = desiredCustomer.Id,
                    Name = desiredCustomer.Name,
                    Age = customerDTO.Age,
                    Address = desiredCustomer.Address,
                    Gender = desiredCustomer.Gender,
                    Points = desiredCustomer.RewardCard.Points,
                    StoreIssued = desiredCustomer.RewardCard.StoreIssued,
                };
            }
        }
    }
}
