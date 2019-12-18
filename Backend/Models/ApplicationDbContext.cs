using System.Data.Entity;
using System.IO;

namespace Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public static readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            $"AttachDbFilename={Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName}" +
                              "\\Backend\\App_Data\\POSDb.mdf;" +
             "Integrated Security=True";
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext() : base(ConnectionString) {}
    }
}

