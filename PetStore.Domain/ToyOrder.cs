namespace PetStore.Domain
{
    public class ToyOrder
    {
        public int ToyId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Toy Toy { get; set; }
        public virtual Order Order { get; set; }
    }
}
