using MeetMe.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Data
{
    public static class ApplicationDbContextSeeder
    {
        public async static  Task SeedRolesAndUsersAsync(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            var roleName = "admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var userName = "admin@microsoft.com";
            if (!await userManager.Users.AnyAsync(x => x.UserName == userName))
            {
                var user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };
                await userManager.CreateAsync(user, "Password1.");
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvder = scope.ServiceProvider;
                var roleManager = serviceProvder.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvder.GetRequiredService<UserManager<ApplicationUser>>();
                var env = serviceProvder.GetRequiredService<IHostEnvironment>();
                var db = serviceProvder.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate(); // veritabanı yoksa oluştur, eksik migration varsa yap
                await SeedRolesAndUsersAsync(roleManager, userManager);
                if (env.IsDevelopment())
                {
                    //2 tane örnek etkinlik ekle
                    SeedMeetings(db);
                    SeedMeetings(db, 121);
                }
            }
            return host;
        }

        private static void SeedMeetings(ApplicationDbContext db)
        {
            if (!db.Meetings.Any())
            {
                db.Meetings.Add(new Meeting()
                {
                    Title = "English Speaking Club",
                    Slug = WebUtilities.URLFriendly("English Speaking Club"),
                    Description = "An English Club is a place for language learners to use English in a casual setting. Practising your skills in the classroom is important.",
                    Place = "Route Cafe, Ankara",
                    MeetingTime = DateTime.Now.AddDays(7),
                    PhotoPath = "meeting1.png"
                });
                db.Meetings.Add(new Meeting()
                {
                    Title = "Environmental Pollution",
                    Slug = WebUtilities.URLFriendly("Environmental Pollution"),
                    Description = "This conference aims to bring together leading academic scientists, researchers and research scholars to exchange and share their experiences and research results on all aspects of Environmental Pollution, Public Health and Impacts. ",
                    Place = "Congresium, Ankara",
                    MeetingTime = DateTime.Now.AddDays(30),
                    PhotoPath = "meeting2.png"
                });
                db.SaveChanges();
            }
        }
        private static void SeedMeetings(ApplicationDbContext db, int count)
        {
            int currentCount = db.Meetings.Count();

            for (int i = currentCount + 1; i <= count; i++)
            {
                db.Meetings.Add(new Meeting()
                {
                    Title = "Meeting " + i,
                    Slug = WebUtilities.URLFriendly("Meeting " + i),
                    Description = "Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.",
                    Place = "Sit amet, consectetur.",
                    MeetingTime = DateTime.Now.AddDays(-i)
                });
                db.SaveChanges();
            }
        }
    }

}
