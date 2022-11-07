namespace PetStore.DataContracts.Orders
{
    public class CreateOrderRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }
        public double Amount { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CreditCard { get; set; }
        public List<OrderByToy> Toys { get; set; }
    }

    public class OrderByToy
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}