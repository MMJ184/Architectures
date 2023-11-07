using Assignment.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<UserToRoles> UserToRoles { get; set; }
    }
}
