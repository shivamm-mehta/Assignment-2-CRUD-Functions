using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Assignment2
{
    public class DataContext : DbContext
    {
        public string DbPath { get; set; }

        public DataContext()
        {
            var path = AppContext.BaseDirectory;
            DbPath = Path.Combine(path, "TestDatabase.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        public DbSet<User> Users { get; set; }
    }
}
