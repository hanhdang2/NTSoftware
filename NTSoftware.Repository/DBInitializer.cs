using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Repository
{
    public class DBInitializer
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, AppDbContext ctx)
        {
            if (userManager.FindByEmailAsync("lhngoc2497@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "lhngoc2497@gmail.com",
                    Email = "lhngoc2497@gmail.com",
                    UserType = Roles.AdminNT
                };

                var result = await userManager.CreateAsync(user, "Ngoc@12345");
                if (result.Succeeded)
                {
                    DetailUser detailUser = new DetailUser
                    {
                        Address = "Hà Nội",
                        Birthday = DateTime.Now,
                        Gender = Gender.Male,
                        Id = user.Id,
                        Name = "Admin",
                        PhoneNumber = "0123456789"
                    };
                    ctx.Set<DetailUser>().Add(detailUser);
                    ctx.SaveChanges();
                }

            }
        }
    }
}
