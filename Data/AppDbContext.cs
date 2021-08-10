using System;

using helping_hand.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using helping_hand.Data.Configurations;

namespace helping_hand.Data
{
    public class AppDbContext : IdentityDbContext<ApiUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<HelpService> HelpServices { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<Notice> Notices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<HelpService>().HasIndex(s => s.Service).IsUnique();
            builder.Entity<Urgency>().HasIndex(u => u.Label).IsUnique();
            builder.Entity<Urgency>().HasIndex(u => u.Level).IsUnique();

            builder.Entity<Request>().HasData(
                new Request { Id = 1, Name = "John Doe", Urgency = "Urgent", Type = "Oxygen" },
                new Request { Id = 2, Name = "Jane Doe", Urgency = "Moderate", Type = "Bed" },
                new Request { Id = 3, Name = "Jim Smith", Urgency = "Not Urgent", Type = "Food" }
            );
        }
    }
}
