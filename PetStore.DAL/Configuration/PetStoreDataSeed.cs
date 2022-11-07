using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Domain;

namespace PetStore.DAL.Configuration
{
    public static class PetStoreDataSeed
    {
        /// <summary>
        /// Reference: https://www.zooplus.hr/shop/psi/igracke_sport_obuka/loptice
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new PetStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<PetStoreDbContext>>());
            if (context.Toys.Any())
                return; // Data was already seeded

            context.OrderStatuses.AddRange(
                new OrderStatus
                {
                    Name = "Placed",
                    Key = "Placed"
                },
                new OrderStatus
                {
                    Name = "Approved",
                    Key = "Approved"
                },
                new OrderStatus
                {
                    Name = "Delivered",
                    Key = "Delivered"
                },
                new OrderStatus
                {
                    Name = "Canceled",
                    Key = "Canceled"
                }
            );

            context.Orders.AddRange(
                new Order
                {
                    FirstName = "Joanna",
                    LastName = "Doe",
                    StatusId = 1,
                    Amount = 56,
                    Street = "Street",
                    StreetNumber = "123",
                    ZipCode = "71000",
                    City = "Sarajevo",
                    CreditCard = "121110987654321"
                },
                new Order
                {
                    FirstName = "John",
                    LastName = "Doe",
                    StatusId = 1,
                    Amount = 24,
                    Street = "Street",
                    StreetNumber = "123",
                    ZipCode = "71000",
                    City = "Sarajevo",
                    CreditCard = "123456789101112"
                }
            );

            context.ToyOrders.AddRange(
                new ToyOrder
                {
                    ToyId = 1,
                    OrderId = 1,
                    Quantity = 1
                },
                new ToyOrder
                {
                    ToyId = 3,
                    OrderId = 1,
                    Quantity = 2
                },
                new ToyOrder
                {
                    ToyId = 2,
                    OrderId = 2,
                    Quantity = 2
                }
            );

            context.ToyCategories.AddRange(
                new ToyCategory
                {
                    Name = "Dogs",
                    Key = "Dogs"
                },
                new ToyCategory
                {
                    Name = "Cats",
                    Key = "Cats"
                },
                new ToyCategory
                {
                    Name = "Birds",
                    Key = "Birds"
                },
                new ToyCategory
                {
                    Name = "Other",
                    Key = "Other"
                }
            );

            context.ToyTypes.AddRange(
                new ToyType
                {
                    Name = "Balls",
                    Key = "Balls"
                },
                new ToyType
                {
                    Name = "Chewing Toys",
                    Key = "CT"
                },
                new ToyType
                {
                    Name = "Mental Stimulation Toys",
                    Key = "MST"
                },
                new ToyType
                {
                    Name = "Whistle Toys",
                    Key = "WT"
                },
                new ToyType
                {
                    Name = "Other",
                    Key = "Other"
                }
            );

            context.Toys.AddRange(
                new Toy
                {
                    Name = "Chuckit! Max Glow Ball",
                    CategoryId = 1,
                    TypeId = 1,
                    Price = 16,
                    Quantity = 5,
                    IsActive = true
                },
                new Toy
                {
                    Name = "Chuckit! Ball Launcher Sport",
                    CategoryId = 1,
                    TypeId = 1,
                    Price = 12,
                    Quantity = 5,
                    IsActive = true
                },
                new Toy
                {
                    Name = "KONG CoreStrength Ball European",
                    CategoryId = 1,
                    TypeId = 2,
                    Price = 20,
                    Quantity = 2,
                    IsActive = true
                },
                new Toy
                {
                    Name = "KONG Wobbler chewing ball",
                    CategoryId = 2,
                    TypeId = 2,
                    Price = 20,
                    Quantity = 2,
                    IsActive = true
                },
                new Toy
                {
                    Name = "Trixie cotton hoop",
                    CategoryId = 3,
                    TypeId = 5,
                    Price = 20,
                    Quantity = 2,
                    IsActive = true
                }
            );

            context.SaveChanges();
        }
    }
}
