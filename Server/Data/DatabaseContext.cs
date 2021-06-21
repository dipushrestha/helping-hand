using Microsoft.EntityFrameworkCore;

using helping_hand.Model;

namespace helping_hand.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasData(
                new Request { Id = 1, Name = "John Doe", Urgency = "Urgent", Type = "Oxygen" },
                new Request { Id = 2, Name = "Jane Doe", Urgency = "Moderate", Type = "Bed" },
                new Request { Id = 3, Name = "Jim Smith", Urgency = "Not Urgent", Type = "Food" }
            );
        }
    }
}
