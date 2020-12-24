using System;
using System.Collections.Generic;
using System.Text;
using AspMvcExample.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMvcExample.Models
{
    public class TradingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptPosition> ReceiptPositions { get; set; }

        public TradingContext()
        {
            
        }

        public TradingContext(DbContextOptions<TradingContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TradingDemo;Integrated Security=True;");
        }
    }
}
