using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Areas.Identity.Data;
using Verlofsite.Models;

namespace Verlofsite.Data
{
    public class IdentiteitsContext : IdentityDbContext<VerlofsiteUser>
    {
        public IdentiteitsContext(DbContextOptions<IdentiteitsContext> options)
            : base(options)
        {
        }
        public DbSet<Verlofsite.Models.Aanvraag> Aanvragen { get; set; }

        public DbSet<VerlofsiteUser> Gebruiker { get; set; }
        public DbSet<Verlofpoule> Verlofpoules { get; set; }
        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<ZiekteAanvraag> ZiekteAanvragen { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
        }

     
    }
}
