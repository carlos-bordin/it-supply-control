using ItSupplyControl.Api.Modules.MasterData;
using Microsoft.EntityFrameworkCore;

namespace ItSupplyControl.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Site> Sites => Set<Site>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Site>(site =>
		{
			site.Property(s => s.Name).HasMaxLength(64);
			site.Property(s => s.Address).HasMaxLength(128);

			site.HasIndex(s => s.Name).IsUnique();
		});
	}
}
