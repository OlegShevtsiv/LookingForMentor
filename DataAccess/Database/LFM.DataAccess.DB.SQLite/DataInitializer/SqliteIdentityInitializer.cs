using System;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;
using LFM.DataAccess.Domain.Services.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LFM.DataAccess.DB.SQLite.DataInitializer
{
    public static class SqliteIdentityInitializer
    {
        public static async Task InitializeSqliteIdentitySeed(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<LfmUser>>();
                    var rolesManager = services.GetRequiredService<RoleManager<LfmRole>>();
                    var lfmRoleManager = services.GetRequiredService<ILfmRoleManager>();

                    await InitializeAsync(userManager, rolesManager, lfmRoleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        private static async Task InitializeAsync(UserManager<LfmUser> userManager, RoleManager<LfmRole> roleManager, ILfmRoleManager lfmRoleManager)
        {
            #region Roles initialization
            
            if (await lfmRoleManager.GetRole(IdentityRoles.Admin) == null)
            {
                await roleManager.CreateAsync(new LfmRole(IdentityRoles.Admin.ToString())
                {
                    Id = (int)IdentityRoles.Admin
                });
            }
            
            if (await lfmRoleManager.GetRole(IdentityRoles.Manager) == null)
            {
                await roleManager.CreateAsync(new LfmRole(IdentityRoles.Manager.ToString())
                {
                    Id = (int)IdentityRoles.Manager
                });
            }
            
            if (await lfmRoleManager.GetRole(IdentityRoles.Mentor) == null)
            {
                await roleManager.CreateAsync(new LfmRole(IdentityRoles.Mentor.ToString())
                {
                    Id = (int)IdentityRoles.Mentor
                });
            }
            
            if (await lfmRoleManager.GetRole(IdentityRoles.Student) == null)
            {
                await roleManager.CreateAsync(new LfmRole(IdentityRoles.Student.ToString())
                {
                    Id = (int)IdentityRoles.Student
                });
            }

            #endregion

            #region Default Users
            
            (string Name, string Email, string Password) defaultAdmin = ("Admin", "admin@mail.com", "123456qwer");
            if (await userManager.FindByNameAsync(defaultAdmin.Name) == null)
            {
                LfmUser admin = new LfmUser { Email = defaultAdmin.Email, UserName = defaultAdmin.Name };
                IdentityResult result = await userManager.CreateAsync(admin, defaultAdmin.Password);
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(admin, IdentityRoles.Admin);
                }
            }
            
            (string Name, string Email, string Password) defaultManager = ("Manager", "manager@mail.com", "123456qwer");
            if (await userManager.FindByNameAsync(defaultManager.Name) == null)
            {
                LfmUser manager = new LfmUser { Email = defaultManager.Email, UserName = defaultManager.Name };
                IdentityResult result = await userManager.CreateAsync(manager, defaultManager.Password);
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(manager, IdentityRoles.Manager);
                }
            }
            
            (string Name, string Email, string Password) defaultMentor = ("Mentor", "mentor@mail.com", "123456qwer");
            if (await userManager.FindByNameAsync(defaultMentor.Name) == null)
            {
                LfmUser mentor = new LfmUser { Email = defaultMentor.Email, UserName = defaultMentor.Name };
                IdentityResult result = await userManager.CreateAsync(mentor, defaultMentor.Password);
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(mentor, IdentityRoles.Mentor);
                }
            }
            
            (string Name, string Email, string Password) defaultStudent = ("Student", "student@mail.com", "123456qwer");
            if (await userManager.FindByNameAsync(defaultStudent.Name) == null)
            {
                LfmUser student = new LfmUser { Email = defaultStudent.Email, UserName = defaultStudent.Name };
                IdentityResult result = await userManager.CreateAsync(student, defaultStudent.Password);
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(student, IdentityRoles.Student);
                }
            }
            
            #endregion
        }
    }
}