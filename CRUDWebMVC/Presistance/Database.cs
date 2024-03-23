using CRUDWebMVC.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebMVC.Presistance;

public class Database : DbContext
{
	public DbSet<Customer> Customers {get; set;}
	public DbSet<Movie> Movies {get; set;}
	public DbSet<MembershipType> MembershipTypes {get; set;}
	public DbSet<Genre> Genres {get; set;}
	
	public Database(DbContextOptions<Database> options) : base(options)
	{
		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>(customer =>
		{
			customer.HasKey(cst => cst.Id);
			customer.Property(cst => cst.Name).IsRequired(true);
			customer.Property(cst => cst.BirthDay).IsRequired(false);
			customer.HasOne(cst => cst.MemberShipType).WithMany(mst => mst.Customers);
		});
		modelBuilder.Entity<Movie>(movie =>
		{
			movie.HasKey(mvo => mvo.Id);
			movie.Property(mvo => mvo.Name).IsRequired(true);
			movie.Property(mvo => mvo.ReleaseDate).IsRequired(true);
			movie.Property(mvo => mvo.DateAdded).IsRequired(true);
			movie.Property(mvo => mvo.Stock).IsRequired(true);
			movie.HasOne(mvo => mvo.Genre).WithMany(gnr => gnr.Movies);
		});
		modelBuilder.Entity<MembershipType>(ship =>
		{
			ship.HasKey(shp => shp.Id);
			ship.Property(shp => shp.Name).IsRequired(true);
			ship.Property(shp => shp.DiscountRate).IsRequired(true);
			ship.Property(shp => shp.DurationInMonths).IsRequired(true);
			ship.Property(shp => shp.SignUpFee).IsRequired(true);
		});
		modelBuilder.Entity<Genre>(genre =>
		{
			genre.HasKey(gnr => gnr.Id);
			genre.Property(gnr => gnr.Name).IsRequired(true);
		});
	}
	
}