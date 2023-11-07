using InfinityCQRS.Backend.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfinityCQRS.App.Database
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser,
                                   ApplicationRole,
                                   string,
                                   IdentityUserClaim<string>,
                                   ApplicationUserRole,
                                   IdentityUserLogin<string>,
                                   IdentityRoleClaim<string>,
                                   IdentityUserToken<string>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
             : base(options)
        { }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }

        #region /*** Tables creating time needed changes ***/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .ToTable("AspNetUser")
                   .Property(p => p.Id)
                   .HasColumnName("Id");

            builder.Entity<ApplicationUserRole>()
                   .ToTable("AspNetUserRole");

            builder.Entity<IdentityUserLogin<string>>()
                   .ToTable("AspNetUserLogin");

            builder.Entity<IdentityUserClaim<string>>()
                   .ToTable("AspNetUserClaim");

            builder.Entity<ApplicationRole>()
                   .ToTable("AspNetRole");

            builder.Entity<IdentityRoleClaim<string>>()
                   .ToTable("AspNetRoleClaim");

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new
                {
                    ur.UserId,
                    ur.RoleId
                });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });
        } 
        #endregion
    }
}
