namespace CRUDWebMVC.Models.Entity;

public class MembershipType
{
	public Guid Id {get; set;} = Guid.NewGuid();
	public string Name {get; set;} = null!;
	public short SignUpFee {get; set;}
	public byte DurationInMonths {get; set;}
	public byte DiscountRate {get; set;}
	public ICollection<Customer>? Customers {get; set;}
}
