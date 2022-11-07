namespace PetStore.Domain
{
    public class ToyType
    {
        public ToyType()
        {
            Toys = new HashSet<Toy>();
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Toy> Toys { get; set; }
    }
}
