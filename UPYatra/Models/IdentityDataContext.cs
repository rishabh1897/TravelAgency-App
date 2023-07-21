using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UPYatra.Models
{
    public class IdentityDataContext: IdentityDbContext
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options): base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
