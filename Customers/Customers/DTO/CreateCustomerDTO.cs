namespace Customers.DTO
{
    public class CreateCustomerDTO
    {
        
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public char Gender { get; set; }
        public int Age { get; set; }

        public int Points { get; set; }

        public string StoreIssued { get; set; }

    }
}
