namespace PetStore.Domain
{
    public class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
