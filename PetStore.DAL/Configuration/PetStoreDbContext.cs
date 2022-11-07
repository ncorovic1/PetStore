using Microsoft.EntityFrameworkCore;
using PetStore.Domain;

namespace PetStore.DAL.Configuration
{
    public class PetStoreDbContext : DbContext
    {
        public PetStoreDbContext(DbContextOptions<PetStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<ToyCategory> ToyCategories { get; set; }
        public DbSet<ToyType> ToyTypes { get; set; }
        public DbSet<ToyOrder> ToyOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "ps");

                entity.HasKey(x => x.Id);
                entity.HasIndex(e => e.FirstName, "IX_Order_FirstName");
                entity.HasIndex(e => e.LastName, "IX_Order_LastName");
                entity.HasIndex(e => e.StatusId, "IX_Order_StatusId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Street).IsRequired().HasMaxLength(50);
                entity.Property(e => e.StreetNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(20);
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreditCard).IsRequired().HasMaxLength(16);

                entity.HasOne(e => e.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Order_StatusId")
                    .IsRequired();
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus", "ps");

                entity.HasKey(x => x.Id);
                entity.HasIndex(e => e.Key, "IX_OrderStatus_Key");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
                entity.Property(e => e.Key).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<Toy>(entity =>
            {
                entity.ToTable("Toy", "ps");

                entity.HasKey(x => x.Id);
                entity.HasIndex(e => e.Name, "IX_Toy_Name");
                entity.HasIndex(e => e.CategoryId, "IX_Toy_CategoryId");
                entity.HasIndex(e => e.TypeId, "IX_Toy_TypeId");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Quantity).IsRequired().HasColumnType("int");
                entity.Property(e => e.Price).IsRequired().HasColumnType("float");
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValueSql("((1))");

                entity.HasOne(e => e.Category)
                    .WithMany(p => p.Toys)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Toy_CategoryId")
                    .IsRequired();

                entity.HasOne(e => e.Type)
                    .WithMany(p => p.Toys)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Toy_TypeId")
                    .IsRequired();
            });

            modelBuilder.Entity<ToyCategory>(entity =>
            {
                entity.ToTable("ToyCategory", "ps");

                entity.HasKey(x => x.Id);
                entity.HasIndex(e => e.Key, "IX_ToyCategory_Key");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
                entity.Property(e => e.Key).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<ToyType>(entity =>
            {
                entity.ToTable("ToyType", "ps");

                entity.HasKey(x => x.Id);
                entity.HasIndex(e => e.Key, "IX_ToyType_Key");

                entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
                entity.Property(e => e.Key).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<ToyOrder>(entity =>
            {
                entity.ToTable("ToyOrder", "ps");

                entity.HasKey(e => new { e.ToyId, e.OrderId });

                entity.HasIndex(e => e.ToyId, "IX_ToyOrder_ToyId");

                entity.HasIndex(e => e.OrderId, "IX_ToyOrder_OrderId");

                entity.HasOne(d => d.Toy)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.ToyId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_ToyOrder_ToyId");

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.Toys)
                      .HasForeignKey(d => d.OrderId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_ToyOrder_OrderId");
            });
        }
    }
}
