using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CRUDWebMVC.Models.Entity;

namespace CRUDWebMVC.Models.ViewModel;

public class MovieFormViewModel
{
	public Guid? Id {get; set;}
	
	[Required(ErrorMessage = "Name is Required")]
	[Display(Name = "Name")]
	public string Name {get; set;} = null!;
	
	[Required(ErrorMessage = "Release Date is Required")]
	[Display(Name = "Release Date")]
	public DateTime ReleaseDate {get; set;}
	
	[Required(ErrorMessage = "Date Added is Required")]
	[Display(Name = "Date of Added")]
	public DateTime DateAdded {get; set;} 
	
	[Required(ErrorMessage = "Stock is Required")]
	[Range(0, int.MaxValue, ErrorMessage = "stock must be large than 0")]
	[Display(Name = "Stock")]
	public int Stock {get; set;}
	
	[Required(ErrorMessage = "Please select one of genres")]
	[Display(Name = "Genre")]
	public Guid GenreId {get; set;}
	
	public IEnumerable<Genre> Genres {get; set;} = null!;
	
}
