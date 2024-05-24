using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocket_MyProject.Models;

namespace Rocket_MyProject.DAL
{
	public class PortfolioContext:IdentityDbContext<AppUser>
	{
        public PortfolioContext(DbContextOptions <PortfolioContext> options):base(options) { }
		public DbSet<Portfolio> Portfolies { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
    }
}
