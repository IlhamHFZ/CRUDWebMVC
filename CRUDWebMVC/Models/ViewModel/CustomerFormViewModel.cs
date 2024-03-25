using System.ComponentModel.DataAnnotations;
using CRUDWebMVC.Models.Entity;

namespace CRUDWebMVC.Models.ViewModel;

public class CustomerFormViewModel
{
	public Guid? Id {get; set;}
	
	[Required(ErrorMessage = "Name is Required")]
	[Display(Name = "Name")]
	public string Name {get; set;} = null!;
	
	[Display(Name = "Date of Birth")]
	public DateTime? BirthDay {get; set;}
	
	[Display(Name = "Subscribed to Newletter")]
	public bool IsSubscribedToNewsletter {get; set;}
	
	[Display(Name = "Membership Type")]
	[Required(ErrorMessage = "Membership is Required")]
	public Guid MembershipTypeId {get; set;}
	
	public IEnumerable<MembershipType> ListMembershipTypes {get; set;} = null!;
}
