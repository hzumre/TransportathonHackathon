
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Infrastructure.DataAccess
{
    public class TransofastDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public TransofastDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
      
            //builder.Entity<AppUser>(x => { x.HasMany(x => x.Transports).WithOne(x => x.AppUser).HasForeignKey(x => x.ID); });
            builder.Entity<AppRole>(eb =>
            {
                eb.HasNoKey();
                eb.HasData(
                new AppRole() { Id = Guid.NewGuid(), Name = "admin", NormalizedName = "ADMIN" },
                new AppRole() { Id = Guid.NewGuid(), Name = "user", NormalizedName = "USER" },
                new AppRole() { Id = Guid.NewGuid(), Name = "employee", NormalizedName = "EMPLOYEE" }
                );
            });

            var tempComp1 = new Company() { ID = 1, Name = "TestCompany1", PhoneNumber = "234567", Address = "test adress", Email = "test.company@gmail.com" };
            var tempComp2 = new Company() { ID = 2, Name = "TestCompany2", PhoneNumber = "234567", Address = "test adress", Email = "test.company@gmail.com" };
            var tempComp3 = new Company() { ID = 3, Name = "TestCompany3", PhoneNumber = "234567", Address = "test adress", Email = "test.company@gmail.com" };
            builder.Entity<Company>(eb =>
            {
                eb.HasData(
                tempComp1,
                tempComp2,
                tempComp3
                );
            });

            var tempEmp1 = new AppUser() { Id = Guid.NewGuid(), UserName = "Employee", Email = "employee@employee.com", PhoneNumber = "12345", CompanyID = tempComp1.ID };
            var tempEmp2 = new AppUser() { Id = Guid.NewGuid(), UserName = "Employee1", Email = "employee1@employee.com", PhoneNumber = "123456", CompanyID = tempComp2.ID };
            var tempEmp3 = new AppUser() { Id = Guid.NewGuid(), UserName = "Employee2", Email = "employee2@employee.com", PhoneNumber = "123457", CompanyID = tempComp3.ID };
            builder.Entity<AppUser>(eb =>
            {
                eb.HasData(
                tempEmp1,
                tempEmp2,
                tempEmp3
                );
            });
            builder.Entity<TransportVehicle>(eb =>
            {
                eb.HasData(
                new TransportVehicle() { ID = 1, Plaka="06-Soso-06", VehicleType=Domain.Enum.VehicleType.Truck, CompanyID=1,AppUserID=tempEmp1.Id },
                new TransportVehicle() { ID = 2, Plaka="06-Soso-02", VehicleType=Domain.Enum.VehicleType.Pickup, CompanyID=1, AppUserID = tempEmp2.Id },
                new TransportVehicle() { ID = 3, Plaka="06-Soso-31", VehicleType=Domain.Enum.VehicleType.Semi, CompanyID=1, AppUserID = tempEmp3.Id }

                );
            });

            base.OnModelCreating(builder);

        }
    }
}
