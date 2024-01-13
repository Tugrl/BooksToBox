using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksToBoxDemo.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "b2080693-07a5-4cab-a8b5-9f5b1f9a8f57";
            var superAdminRoleId = "d82af1a3-92bf-4ba0-b66b-e7cfb5ee1333";
            var userRoleId = "756b74eb-41c3-4422-97aa-651e8f23ef77";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId

                }, new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id= userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var superAdminId = "af8881af-8e0d-40e9-b83c-2bc3e05471da";
            var superAdminUser = new IdentityUser
            {
                UserName = "bookstoboxdemo@btb.com",
                Email= "bookstoboxdemo@btb.com",
                NormalizedEmail= "bookstoboxdemo@btb.com".ToUpper(),
                NormalizedUserName= "bookstoboxdemo@btb.com".ToUpper(),
                Id =superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"Bookstoboxdemo_123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId=superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
