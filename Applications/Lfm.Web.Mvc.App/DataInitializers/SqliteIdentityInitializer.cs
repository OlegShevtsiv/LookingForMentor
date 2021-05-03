using System;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;
using Lfm.Domain.Common.Services.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lfm.Web.Mvc.App.DataInitializers
{
    public static class SqliteIdentityInitializer
    {
        public static async Task Init(IServiceProvider serviceProvider)
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
            
            if (await lfmRoleManager.GetRole(LfmIdentityRolesEnum.Admin) == null)
            {
                await roleManager.CreateAsync(new LfmRole(LfmIdentityRolesEnum.Admin.ToString())
                {
                    Id = (int)LfmIdentityRolesEnum.Admin
                });
            }
            
            if (await lfmRoleManager.GetRole(LfmIdentityRolesEnum.Manager) == null)
            {
                await roleManager.CreateAsync(new LfmRole(LfmIdentityRolesEnum.Manager.ToString())
                {
                    Id = (int)LfmIdentityRolesEnum.Manager
                });
            }
            
            if (await lfmRoleManager.GetRole(LfmIdentityRolesEnum.Mentor) == null)
            {
                await roleManager.CreateAsync(new LfmRole(LfmIdentityRolesEnum.Mentor.ToString())
                {
                    Id = (int)LfmIdentityRolesEnum.Mentor
                });
            }
            
            if (await lfmRoleManager.GetRole(LfmIdentityRolesEnum.Student) == null)
            {
                await roleManager.CreateAsync(new LfmRole(LfmIdentityRolesEnum.Student.ToString())
                {
                    Id = (int)LfmIdentityRolesEnum.Student
                });
            }

            #endregion

            #region Default Users
            
            string defaultAdminEmail = "admin@mail.com";
            if (await userManager.FindByEmailAsync(defaultAdminEmail) == null)
            {
                LfmUser admin = new LfmUser
                {
                    Name = "Admin",
                    Email = defaultAdminEmail,
                    UserName = defaultAdminEmail
                };
                IdentityResult result = await userManager.CreateAsync(admin, "123456qwer");
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(admin, LfmIdentityRolesEnum.Admin);
                }
            }
            
            string defaultManagerEmail = "manager@mail.com";
            if (await userManager.FindByEmailAsync(defaultManagerEmail) == null)
            {
                LfmUser manager = new LfmUser
                {
                    Name = "Manager",
                    Email = defaultManagerEmail,
                    UserName = defaultManagerEmail
                };
                IdentityResult result = await userManager.CreateAsync(manager, "123456qwer");
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(manager, LfmIdentityRolesEnum.Manager);
                }
            }
            
            string defaultMentorEmail = "mentor@mail.com";
            if (await userManager.FindByEmailAsync(defaultMentorEmail) == null)
            {
                LfmUser mentor = new LfmUser
                {
                    Name = "Mentor",
                    Email = defaultMentorEmail, 
                    UserName = defaultMentorEmail
                };
                IdentityResult result = await userManager.CreateAsync(mentor, "123456qwer");
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(mentor, LfmIdentityRolesEnum.Mentor);
                }
            }
            
            string defaultStudentEmail = "student@mail.com";
            if (await userManager.FindByEmailAsync(defaultStudentEmail) == null)
            {
                LfmUser student = new LfmUser
                {
                    Name = "Student",
                    Email = defaultStudentEmail, 
                    UserName = defaultStudentEmail
                };
                IdentityResult result = await userManager.CreateAsync(student, "123456qwer");
                if (result.Succeeded)
                {
                    await lfmRoleManager.SetRoleToUser(student, LfmIdentityRolesEnum.Student);
                }
            }
            
            #endregion
        }
    }
}