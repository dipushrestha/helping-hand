using System;

using helping_hand.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace helping_hand.Data
{
    public class AppDbContext : IdentityDbContext<ApiUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Request>().HasData(
                new Request { Id = 1, Name = "John Doe", Urgency = "Urgent", Type = "Oxygen" },
                new Request { Id = 2, Name = "Jane Doe", Urgency = "Moderate", Type = "Bed" },
                new Request { Id = 3, Name = "Jim Smith", Urgency = "Not Urgent", Type = "Food" }
            );
        }
    }
}
