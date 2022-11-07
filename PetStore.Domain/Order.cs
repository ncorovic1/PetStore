namespace PetStore.Domain
{
    public class Order
    {
        public Order()
        {
            Toys = new HashSet<ToyOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }
        public double Amount { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CreditCard { get; set; }

        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<ToyOrder> Toys { get; set; }
    }
}
