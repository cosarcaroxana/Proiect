using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeModel.Models;

namespace OfficeModel.Data
{
    public class OfficeContext : DbContext
    {
        public OfficeContext(DbContextOptions<OfficeContext> options): base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorService> DoctorServices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<DoctorService>().ToTable("DoctorService");
            modelBuilder.Entity<DoctorService>()
            .HasKey(c => new { c.ServiceID, c.DoctorID });//configureaza cheia
            //primara compusa

        }
    }
}
