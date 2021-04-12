using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Manitouage1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public IList<Invoice> invoices {
            get; set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ManitouageDbContext: IdentityDbContext<ApplicationUser>
    {
        public ManitouageDbContext()
            : base( "ManitouageDb", throwIfV1Schema: false)
        {
        }

        public static ManitouageDbContext Create()
        {
            return new ManitouageDbContext();
        }

        public DbSet<Product> products {
            get; set;
        }

        public DbSet<Invoice> invoices {
            get; set;
        }

        public DbSet<ProductXInvoice> productXInvoices {
            get; set;
        }

        public DbSet<Testimonial> testimonials
        {
            get; set;
        }

        public DbSet<Volunteer> volunteers
        {
            get; set;
        }

        public DbSet<Department> departments
        {
            get; set;
        }

   
        public DbSet<JobPosting> jobPostings
        {
            get; set;
        }

        public DbSet<Event> events
        {
            get; set;
        }
        public DbSet<Alert> alerts
        {
            get; set;
        }

        public DbSet<Donation> donations
        {
            get; set;
        }

        public DbSet<ContactUs> contactus
        {
            get; set;
        }


    }
}