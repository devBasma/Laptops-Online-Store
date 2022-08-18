﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Job_Offers_MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
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
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Job_Offers_MVC.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_MVC.Models.Jobs> Jobs { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_MVC.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_MVC.Models.ApplyForJob> ApplyForJobs { get; set; }

    }
}