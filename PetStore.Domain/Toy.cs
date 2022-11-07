namespace PetStore.Domain
{
    public class Toy
    {
        public Toy()
        {
            Orders = new HashSet<ToyOrder>();
            IsActive = true;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? Rating { get; set; }
        public bool? IsActive { get; set; }

        public virtual ToyCategory Category { get; set; }
        public virtual ToyType Type { get; set; }
        public virtual ICollection<ToyOrder> Orders { get; set; }
    }
}
