using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocket_MyProject.Models;

namespace Rocket_MyProject.DAL
{
	public class PortfolioContext:IdentityDbContext
	{
        public PortfolioContext(DbContextOptions <PortfolioContext> options):base(options) { }
		public DbSet<Portfolio> Portfolies { get; set; }
    }
}
