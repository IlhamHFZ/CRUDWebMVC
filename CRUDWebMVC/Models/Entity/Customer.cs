namespace CRUDWebMVC.Models.Entity;

public class Customer
{
	public Guid Id {get; set;} = Guid.NewGuid();
	public string Name {get; set;} = null!;
	public DateTime? BirthDay {get; set;}
	public bool IsSubscribedToNewsletter {get; set;}
	public MembershipType MemberShipType {get; set;} = null!;
}
