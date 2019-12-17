using System.Data.Entity;

namespace Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                                "AttachDbFilename=|DataDirectory|POSDb.mdf;" +
                                                "Integrated Security=True";
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext() : base(ConnectionString) { }
    }
}

