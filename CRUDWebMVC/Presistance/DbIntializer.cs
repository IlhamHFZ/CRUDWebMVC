using CRUDWebMVC.Models.Entity;

namespace CRUDWebMVC.Presistance;

public static class DbInitializer
{
	public static async Task Initialize(Database context)
	{
		if(context.Customers.Any() && context.Movies.Any()
			&& context.Genres.Any() && context.MembershipTypes.Any())
		{
			return;
		}
		
		var memberShip = new MembershipType[]
		{
			new MembershipType(){Name = "Pay as you go", SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0},
			new MembershipType(){Name = "Monthly", SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10},
			new MembershipType(){Name = "Half Semester", SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15},
			new MembershipType(){Name = "Anually", SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20}
		};
		
		var customers = new Customer[]
		{
			new Customer(){Name = "Ilham Hafidz Fahry", BirthDay = new DateTime(2001, 06,14), IsSubscribedToNewsletter = false, MemberShipType = memberShip[0]},
			new Customer(){Name = "Hatsune Miku", BirthDay = new DateTime(2007, 08, 31), IsSubscribedToNewsletter = true, MemberShipType = memberShip[1]},
			new Customer(){Name = "Rafeala", IsSubscribedToNewsletter = true, MemberShipType = memberShip[2]},
			new Customer(){Name = "Naruto Uzumaki", IsSubscribedToNewsletter = false, MemberShipType = memberShip[3]},
			new Customer(){Name = "Meguri Luka", BirthDay = new DateTime(2009, 01, 30), IsSubscribedToNewsletter = true, MemberShipType = memberShip[3]},
			new Customer(){Name = "Kagamine Rin", BirthDay = new DateTime(2007, 12, 27), IsSubscribedToNewsletter = false, MemberShipType = memberShip[2]},
			new Customer(){Name = "Kagamine Len", BirthDay = new DateTime(2007, 12, 27), IsSubscribedToNewsletter = false, MemberShipType = memberShip[2]}
		};
		
		var genres = new Genre[]
		{
			new	Genre(){Name = "Anime"},
			new Genre(){Name = "Adventure"},
			new Genre(){Name = "Romance"},
			new Genre(){Name = "Child & Family"},
			new Genre(){Name = "Shoujo"},
			new Genre(){Name = "Shounen"},
			new Genre(){Name = "Movie"}
		};
		
		var movies = new Movie[]
		{
			new Movie()
			{
				Name = "Naruto", 
				ReleaseDate = new DateTime(1999, 02, 15), 
				DateAdded = new DateTime(2023, 03, 13), 
				Stock = 10, 
				Genre = genres[1]
			},
			new Movie()
			{
				Name = "Naruto Shippuden",
				ReleaseDate = new DateTime(2002, 05, 20),
				DateAdded = new DateTime(2023, 03, 13),
				Stock = 10,
				Genre = genres[1]
			},
			new Movie()
			{
				Name = "Avatar The Last Air Bendder",
				ReleaseDate = new DateTime(2005, 08, 01),
				DateAdded = new DateTime(2020, 09, 01),
				Stock = 5,
				Genre = genres[3]
			},
			new Movie()
			{
				Name = "5 cm per second",
				ReleaseDate = new DateTime(1998, 06, 14),
				DateAdded = new DateTime(2024, 01, 15),
				Stock = 15,
				Genre = genres[6]
			},
			new Movie()
			{
				Name = "Sakura Cardcaptor",
				ReleaseDate = new DateTime(1997, 03, 27),
				DateAdded = new DateTime(2021, 09, 20),
				Stock = 5,
				Genre = genres[4]
			},
			new Movie()
			{
				Name = "Eyeshield 21",
				ReleaseDate = new DateTime(2007, 07, 23),
				DateAdded = new DateTime(2021, 03, 16),
				Stock = 5,
				Genre = genres[5]
			}
		};
		
		await context.MembershipTypes.AddRangeAsync(memberShip);
		await context.Customers.AddRangeAsync(customers);
		await context.Genres.AddRangeAsync(genres);
		await context.Movies.AddRangeAsync(movies);
		
		await context.SaveChangesAsync();
	}
}
