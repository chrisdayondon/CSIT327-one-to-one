namespace Customers.Models
{
    public class RewardCard
    {
        public int Id { get; set; }

        public int Points { get; set; }

        public string StoreIssued { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

    }
}
