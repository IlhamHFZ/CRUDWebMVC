namespace CRUDWebMVC.Models.Entity;

public class Movie
{
	public Guid Id {get; set;} = Guid.NewGuid();
	public string Name {get; set;} = null!;
	public DateTime ReleaseDate {get; set;} 
	public DateTime DateAdded {get; set;}
	public int Stock {get; set;}
	public Genre Genre {get; set;} = null!;
}
