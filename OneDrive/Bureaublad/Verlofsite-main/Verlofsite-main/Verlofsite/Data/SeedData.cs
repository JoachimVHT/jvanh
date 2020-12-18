using Verlofsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Verlofsite.Authorization;
using Microsoft.AspNetCore.Identity;
using Verlofsite.Areas.Identity.Data;

// dotnet aspnet-codegenerator razorpage -m Aanvraag -dc IdentiteitsContext -udl -outDir Pages\Aanvragen --referenceScriptLibraries

namespace Verlofsite.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new IdentiteitsContext(
                serviceProvider.GetRequiredService<DbContextOptions<IdentiteitsContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@milcobel.com");
                await EnsureRole(serviceProvider, adminID, Constants.AanvraagAdministratorsRole);

                // allowed user can create and edit aanvragen that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "ploegchef@milcobel.com");
                await EnsureRole(serviceProvider, managerID, Constants.AanvraagManagersRole);

                var HRID = await EnsureUser(serviceProvider, testUserPw, "HR@milcobel.com");
                await EnsureRole(serviceProvider, HRID, Constants.AanvraagHRsRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<VerlofsiteUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Verlofsite.Areas.Identity.Data.VerlofsiteUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<Verlofsite.Areas.Identity.Data.VerlofsiteUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }


        public static void SeedDB(IdentiteitsContext context, string adminID)
        {
            if (context.Aanvragen.Any())
            {
                return;   // DB has been seeded
            }

            context.Aanvragen.AddRange(
                new Aanvraag
                {
                    WerknemerNaam = " testpersoon",
                    
                    Soort = SOORT.ADV,
                    OwnerID = adminID
                }
            
             );
            context.SaveChanges();
        }

    }
}