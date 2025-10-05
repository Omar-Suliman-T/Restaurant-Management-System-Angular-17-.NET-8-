using Microsoft.EntityFrameworkCore;
using My_Resturant.Entities;
using My_Resturant.EntityConfigurations;
using My_Resturant.EntityConfigurationszz;
using My_Resturant.Helper;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace My_Resturant.Context
{
    public class RestDbContext : DbContext
    {
        public RestDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingrediant> ingrediants { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<OrderItemDetails> OrderItemDetails { get; set; }
        public DbSet<OrderMealDetiales> OrderMealDetails { get; set; }
        public DbSet<mealDetials> MealDetials { get; set; }
        public DbSet<IngrediantItem> IngrediantItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Code> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LookupType>().HasData(
            new LookupType { id = 1, name = "CategoryName" },
            new LookupType { id = 2, name = "Status" },
            new LookupType { id = 3, name = "Unit" },
            new LookupType { id=4 ,name="Role"},
            new LookupType {id=5,name="Rateing" });

            modelBuilder.Entity<LookupItem>().HasData(
            new LookupItem { id = 1, name = "Fast Food", lookupTypeID = 1 },
            new LookupItem { id = 2, name = "Italian", lookupTypeID = 1 },
            new LookupItem { id = 3, name = "Healty", lookupTypeID = 1 },
            new LookupItem { id = 4, name = "New", lookupTypeID = 2 },
            new LookupItem { id = 5, name = "Confirmed", lookupTypeID = 2 },
            new LookupItem { id = 6, name = "Cancelled", lookupTypeID = 2 },
            new LookupItem { id = 7, name = "Delivered", lookupTypeID = 2 },
            new LookupItem { id = 18, name = "Completed", lookupTypeID = 2 },
            new LookupItem { id = 8, name = "Gm", lookupTypeID = 3 },
            new LookupItem { id = 9, name = "Ml", lookupTypeID = 3 },
            new LookupItem { id = 10, name = "Pieces", lookupTypeID = 3 },
            new LookupItem { id= 11,name="Admin",lookupTypeID=4},
            new LookupItem { id=12, name="customer",lookupTypeID=4},
            new LookupItem { id = 13, name = "Very bad", lookupTypeID = 5 },
            new LookupItem { id = 14, name = "Bad", lookupTypeID = 5 },
            new LookupItem { id = 15, name = "Good", lookupTypeID = 5 },
            new LookupItem { id = 16, name = "Very Good", lookupTypeID = 5 },
            new LookupItem { id = 17, name = "Excellent", lookupTypeID = 5 });

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    id = 1,
                    firstName = "Omar",
                    lastName = "Suliman",
                    email = EncryptionHelper.GenerateSHA384String("omartam22@gmail.com"),
                    password = EncryptionHelper.GenerateSHA384String("345%dev"),
                    role = 11,
                    creationDate = new DateTime(2025, 3, 29, 0, 0, 0, DateTimeKind.Utc),
                    isActive = true,
                    phone = "079428423"
                });
                


            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new IngrediantConfigurations());
            modelBuilder.ApplyConfiguration(new ItemConfigurations());
            modelBuilder.ApplyConfiguration(new LookupItemConfigurations());
            modelBuilder.ApplyConfiguration(new LookupTypeConfigurations());
            modelBuilder.ApplyConfiguration(new MealConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new PersonConfigurations());
            modelBuilder.ApplyConfiguration(new IngrediantItemConfigurations());
            modelBuilder.ApplyConfiguration(new MealDetialsConfigurations());
            modelBuilder.ApplyConfiguration(new OrderItemDetailsConfigurations());
            modelBuilder.ApplyConfiguration(new OrderMealDetialesConfigurations());
            modelBuilder.ApplyConfiguration(new ReservationConfigurations());
            modelBuilder.ApplyConfiguration(new CodeConfiguration());

        }
    }
}
