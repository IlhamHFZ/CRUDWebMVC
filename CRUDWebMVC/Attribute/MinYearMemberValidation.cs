using System.ComponentModel.DataAnnotations;
using CRUDWebMVC.Models.Entity;

namespace CRUDWebMVC.Attribute;

public class MinYearMemberValidation : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		var customer = (Customer) validationContext.ObjectInstance;
		
		if(customer.MemberShipType.Name == "Pay as you go" || customer.MemberShipType.Name == "")
		{
			return ValidationResult.Success;
		}
		
		if(customer.BirthDay == null)
		{
			return new ValidationResult("Birthdate is required");
		}
		
		var age = DateTime.Today.Year - customer.BirthDay.Value.Year;
		
		return age >= 18 ? ValidationResult.Success : new ValidationResult("Customer should be at least 18 year old");
	}
}
