using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerRelationshipManagment.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerRelationshipManagment.db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CRMDatabase2.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 2,                 
                    Name = "jane Doe",
                    Phone = "123-456-7890",
                    Address = "123 Elm St",
                    Frequency = "Weekly",
                    Price = 150.00m,
                    DateLastMowed = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)),
                    DateScheduled = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                    Acres = 2.5,
                    Notes = "First-time customer",                 
                    RecentHoursMowing = 5,
                    HistoricHoursMowing = new List<int> { 5, 6, 4 },                   
                }
            );
            modelBuilder.Entity<Lead>().HasData(
                new Lead
                {
                    Id =1,
                    Name = "John Smith",
                    Phone = "987-654-3210",
                    Address = "456 Oak St",
                    Frequency = "Bi-Weekly",
                    QuotePrice = 200.00m,
                    Acres = 3.0,
                    DateScheduled = DateOnly.FromDateTime(DateTime.Now.AddDays(14)),
                    Notes = "Interested in lawn care services",
                }
            );
        }

    }
}
