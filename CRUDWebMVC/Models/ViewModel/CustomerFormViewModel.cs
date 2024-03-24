using System.ComponentModel.DataAnnotations;
using CRUDWebMVC.Models.Entity;

namespace CRUDWebMVC.Models.ViewModel;

public class CustomerFormViewModel
{
	[Required]
	[Display(Name = "Name")]
	public string Name {get; set;} = null!;
	
	[Display(Name = "Date of Birth")]
	public DateTime BirthDay {get; set;}
	
	[Display(Name = "Subscribed to Newletter")]
	public bool IsSubscribedToNewsletter {get; set;}
	
	[Display(Name = "Membership Type")]
	public IEnumerable<MembershipType> MembershipTypes {get; set;} = null!;
}
