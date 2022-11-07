namespace PetStore.Abstraction.DAL
{
    public class SearchToysFilter
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public bool? IsActive { get; set; }
    }
}
