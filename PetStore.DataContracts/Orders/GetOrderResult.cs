using PetStore.DataContracts.ToyOrders;

namespace PetStore.DataContracts.Orders
{
    public class GetOrderResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CreditCard { get; set; }
        public List<GetToyOrderResult> Toys { get; set; }
    }
}