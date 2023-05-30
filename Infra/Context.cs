using complete_gamer_project.Models;
using Microsoft.EntityFrameworkCore;

namespace complete_gamer_project.Infra
{
	public class Context : DbContext
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<Team> Teams { get; set; }
		
		public Context()
		{
		}
		
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
		{
			if (!optionsBuilder.IsConfigured) 
			{
				// Connection string
				// Data source is the database manager server name
				// Initial catalog is the database name
				// Integrated security make the system authenticate the database manager
				// Trust server certificate 
				optionsBuilder.UseSqlServer("Data Source = NOTE21-S15; Initial Catalog = gamerProjectDb; User Id = sa; pwd = Senai@134; TrustServerCertificate = True");
			}
		}
	}
}