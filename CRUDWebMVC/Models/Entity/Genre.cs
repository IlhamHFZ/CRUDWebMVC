namespace CRUDWebMVC.Models.Entity;

public class Genre
{
	public Guid Id {get; set;} = Guid.NewGuid();
	public string Name {get; set;} = null!;
	public ICollection<Movie>? Movies {get; set;}
}
