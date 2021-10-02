using BalancR.Common;
using BalancR.Models.CosmosDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BalancR.Services
{
    public class CosmosContext : DbContext
    {
        private readonly IConfiguration _config;

        public CosmosContext(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<AccountBalance> AccountBalances { get; set; }
        public virtual DbSet<Benchmark> Benchmarks { get; set; }
        public virtual DbSet<TriggerAlert> TriggerAlerts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(_config[AppSettings.cosmosDbPrimaryConnectionString], "BalancR");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.Entity<Transaction>()
                .HasPartitionKey(t => t.Id)
                .ToContainer("transactions");

            modelBuilder.Entity<AccountBalance>()
                .HasPartitionKey(t => t.Id)
                .ToContainer("accountBalance");            
            
            modelBuilder.Entity<Benchmark>()
                .HasPartitionKey(t => t.Id)
                .ToContainer("benchmark");

            modelBuilder.Entity<TriggerAlert>()
                .HasPartitionKey(t => t.Id)
                .ToContainer("triggerAlert");
        }
    }
}
