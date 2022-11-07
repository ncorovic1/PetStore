namespace PetStore.Abstraction.DAL
{
    public class SearchOrdersFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? StatusId { get; set; }
        public string City { get; set; }
    }
}
