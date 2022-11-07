namespace PetStore.DataContracts.Orders
{
    public class SearchOrdersRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? StatusId { get; set; }
        public string City { get; set; }
    }
}