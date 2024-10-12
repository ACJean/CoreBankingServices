using CustomerOperations.Infrastructure.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOperations.Infrastructure.EF
{
    public class CustomerDbContext : DbContext
    {

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<DbCustomer> Customers { get; set; }
        public DbSet<DbPerson> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la tabla Person
            modelBuilder.Entity<DbPerson>(entity =>
            {
                //entity.ToTable("Person");  // Nombre de la tabla

                //entity.HasKey(p => p.Id);  // Clave primaria
                //entity.Property(p => p.Id).HasColumnName("Per_Id");

                //entity.Property(p => p.Name)
                //      .HasColumnName("Per_Name")
                //      .HasMaxLength(100)
                //      .IsRequired();

                //entity.Property(p => p.Gender)
                //      .HasColumnName("Per_Gender")
                //      .HasMaxLength(10)
                //      .IsRequired();

                //entity.Property(p => p.Age)
                //      .HasColumnName("Per_Age")
                //      .IsRequired();

                //entity.Property(p => p.IdentityNumber)
                //      .HasColumnName("Per_IdentityNumber")
                //      .HasMaxLength(50)
                //      .IsRequired();

                //entity.Property(p => p.Address)
                //      .HasColumnName("Per_Address")
                //      .HasMaxLength(200);

                //entity.Property(p => p.PhoneNumber)
                //      .HasColumnName("Per_PhoneNumber")
                //      .HasMaxLength(20);
            });

            // Configuración para la tabla Customer
            modelBuilder.Entity<DbCustomer>(entity =>
            {
                //entity.ToTable("Customer");  // Nombre de la tabla

                //entity.HasKey(c => c.Id);  // Clave primaria
                //entity.Property(c => c.Id).HasColumnName("Cus_Id");

                //entity.Property(c => c.Password)
                //      .HasColumnName("Cus_Password")
                //      .HasMaxLength(100)
                //      .IsRequired();

                //entity.Property(c => c.State)
                //      .HasColumnName("Cus_State")
                //      .IsRequired();

                //// Relación 1:1 con Person (Foreign Key)
                //entity.HasOne(c => c.Person)
                //      .WithOne(p => p.Customer)
                //      .HasForeignKey<DbCustomer>(c => c.PersonId)
                //      .HasConstraintName("FK_Customer_Person");
            });
        }

    }
}
