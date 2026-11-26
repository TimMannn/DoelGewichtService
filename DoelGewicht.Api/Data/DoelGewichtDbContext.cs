using DoelGewicht.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DoelGewicht.Api.Data;

// Eigen database (DoelGewicht) met alleen de doelgewicht-tabel; migraties worden vanuit deze service gedaan.
public class DoelGewichtDbContext : DbContext
{
    public DoelGewichtDbContext(DbContextOptions<DoelGewichtDbContext> options) : base(options) { }

    public DbSet<DoelGewichtModel> DoelGewichten { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Geen foreign key naar de gebruikers (die staan in de hoofdapp), wel een index voor snelle opzoeking per user.
        modelBuilder.Entity<DoelGewichtModel>().HasIndex(d => d.UserId);
    }
}
