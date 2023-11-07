using Assignment.Database;
using Assignment.Database.Entities;
using Assignment.Repository.Common;
using BC = BCrypt.Net.BCrypt;

namespace Assignment.Api.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            context.Database.EnsureCreated();

            //if (env.IsDevelopment())
            //    context.Database.Migrate();

            //Initialize your tables here
            SeedDataForRoles(context);
        }

        private static void SeedDataForRoles(DatabaseContext _context)
        {
            if (!_context.Roles.Any(m => m.Name == UserRoles.SuperAdmin))
                _context.Roles.Add(new Roles { Name = UserRoles.SuperAdmin });
           
            if (!_context.Roles.Any(m => m.Name == UserRoles.User))
                _context.Roles.Add(new Roles { Name = UserRoles.User });


            _context.SaveChanges();

            if (!_context.Users.Any(m => m.Email == "superadmin@gmail.com"))
                _context.Users.Add(new Users
                {
                    Name = UserRoles.SuperAdmin,
                    Email = "superadmin@gmail.com",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1,
                    PasswordHash = BC.HashPassword("Adm!n"),
                });
            _context.SaveChanges();
        }
    }
}
