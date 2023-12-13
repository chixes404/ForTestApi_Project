using Microsoft.EntityFrameworkCore;
using fotTestAPI.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using fotTestAPI.Model.Authentication;

namespace fotTestAPI.DbContexts
{
    public class CityInfoContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<City> Cities { get; set; }
		public DbSet<PointOfInterests>PointOfInterests { get; set; }
	
		public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure the primary key for the City entity (assuming it's "Id").
			modelBuilder.Entity<City>().HasKey(c => c.Id);
			modelBuilder.Entity<PointOfInterests>().HasKey(c => c.Id);
			// Seed data for the City entity.
		

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>().HasData(

				new City
				{
					Id = 1,
					Name = "Cairo",
					Description = "Cairo is fuckin awful city"


				}
				);

			modelBuilder.Entity<PointOfInterests>().HasData(
				new PointOfInterests
				{

					Id = 1,
					Name = "Pyramids",
					Description = "Pyramids is fuckin good history",
					CityId_v2 = 1


				}
				);



		}

	}
}
