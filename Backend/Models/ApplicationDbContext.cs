using System.Data.Entity;

namespace Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public static string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                                "AttachDbFilename=|DataDirectory|POSDb.mdf;" +
                                                "Integrated Security=True";
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UserHistory> UsersHistory { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public ApplicationDbContext() : base(ConnectionString) {}
    }
}

