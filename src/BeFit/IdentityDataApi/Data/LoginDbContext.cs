using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityDataApi.Data
{
    public class LoginDbContext : IdentityDbContext<AppUser>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }
    }
}
