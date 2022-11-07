namespace PetStore.DataContracts.Toys
{
    public class SearchToysRequest
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public bool? IsActive { get; set; }
    }
}
