using APIDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ItemMaster> ItemMasters { get; set; }
        public DbSet<ItemMasterView> ItemMasterSqlView { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemMasterView>(entity =>
            {
                entity.HasKey(e => e.ID);    // primary key
                entity.ToTable("vMasterItem"); // sql view name
            });

        }
    } 
}
