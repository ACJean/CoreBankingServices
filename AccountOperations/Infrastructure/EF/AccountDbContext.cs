using AccountOperations.Infrastructure.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOperations.Infrastructure.EF
{
    public class AccountDbContext : DbContext
    {

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        public DbSet<DbAccount> Accounts { get; set; }
        public DbSet<DbMovements> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbAccount>();

            modelBuilder.Entity<DbMovements>();
        }

    }
}
