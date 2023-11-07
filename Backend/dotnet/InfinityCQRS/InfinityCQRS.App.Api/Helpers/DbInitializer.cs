using InfinityCQRS.App.Database;

namespace Cycle.App.Api.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            context.Database.EnsureCreated();

            //if (env.IsDevelopment())
            //    context.Database.Migrate();

            //Initialize your tables here
            SeedData(context);
        }

        private static void SeedData(DatabaseContext _context)
        {
            //if (!_context.AspNetRoles.Any(m => m.Name == UserRoles.SuperAdmin))
            //    _context.AspNetRoles.Add(new ApplicationRole { Name = UserRoles.SuperAdmin, DisplayName = "Super Admin", CreatedBy = 1, CreatedDate = DateTime.UtcNow });
            //if (!_context.AspNetRoles.Any(m => m.Name == UserRoles.User))
            //    _context.AspNetRoles.Add(new ApplicationRole { Name = UserRoles.User, DisplayName = "User", CreatedBy = 1, CreatedDate = DateTime.UtcNow });

            //_context.SaveChanges();

            //if (!_context.AspNetUsers.Any(m => m.Email == "superadmin@gmail.com"))
            //    _context.Users.Add(new Users
            //    {
            //        Name = UserRoles.SuperAdmin,
            //        Email = "superadmin@gmail.com",
            //        MobileNumber = "",
            //        PasswordHash = BC.HashPassword("Adm!n123"),
            //        CountryId = "India",
            //        ProfilePicturePath = "",
            //        UserToRoles = new List<UserToRoles> {
            //            new UserToRoles {
            //                RolesId = 1,
            //                UsersId = 1,
            //                IsDisabled = false,
            //                CreatedDate = DateTime.UtcNow,
            //                CreatedBy = 1,
            //            }
            //        },
            //        CreatedBy = 0,
            //        CreatedDate = DateTime.UtcNow
            //    });

            _context.SaveChanges();
        }
    }
}
