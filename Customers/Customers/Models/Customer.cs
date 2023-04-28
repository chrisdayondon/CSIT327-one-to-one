namespace Customers.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public char Gender { get; set; }
        public int Age { get; set; }
    }
}
