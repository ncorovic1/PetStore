namespace PetStore.DataContracts.Toys
{
    public class CreateToyRequest
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
