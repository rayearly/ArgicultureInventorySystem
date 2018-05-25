using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArgicultureInventorySystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("ID Number")]
        public string IdNumber { get; set; }

        public string Name { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNo { get; set; }

        public virtual IList<Booking> Bookings { get; set; }

        public string DepartmentFacultyName { get; set; }

        [DisplayName("Department Name")]
        public int? DFId { get; set; }

        [ForeignKey("DFId")]
        public virtual DepartmentFaculty DepartmentFaculty { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UniversityCommunity> UniversityCommunities { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<StockMeasurement> StockMeasurements { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<BookingDate> BookingDates { get; set; }
        public DbSet<DepartmentFaculty> DepartmentFaculties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<ArgicultureInventorySystem.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}