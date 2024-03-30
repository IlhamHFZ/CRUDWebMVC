using CRUDWebMVC.Models.Entity;
using CRUDWebMVC.Models.ViewModel;
using CRUDWebMVC.Presistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebMVC.Controllers;

[Route("Customers")]
public class CustomersController : Controller
{
	private readonly Database _db;
	public CustomersController(Database db)
	{
		_db = db;
	}
	
	[Route("Index")]	
	public async Task<ActionResult> Index()
	{
		/*
		entity framework hanya load object yang dipanggilnya tapi tidak dengan hubungan objectnya.
		jadi misalkan ada Customer punya hubungan dengan MemberShipType One to Many dan yang dipanggil hanya Customer
		maka hubungan customers dengan MemberShipType tidak di panggil.
		untuk mengatasi permaslahan kita dapat menggunakan eager loading.
		eager loading adalah sebuah proses ketika memanggil sebuah object maka hubungan dengan object lainnya 
		akan terpanggil. teknisnya dengan perintah .Include(object lain yang mau dipanggil)
		*/
		var customers = await _db.Customers.Include(cst => cst.MemberShipType).OrderBy(cst => cst.Name).ToListAsync();
		return View(customers);
	}
	
	[Route("Detail/{Id:Guid}")]
	public async Task<ActionResult> Detail([FromRoute] Guid Id)
	{
		var customer = await _db.Customers.FirstOrDefaultAsync(cst => cst.Id == Id);
		if(customer == null)
		{
			return NotFound();
		}
		return View(customer);
	}
	
	[HttpGet]
	[Route("Create")]
	public async Task<ActionResult> Create()
	{
		var membershipType = await _db.MembershipTypes.ToListAsync();
		var viewModel = new CustomerFormViewModel()
		{
			ListMembershipTypes = membershipType
		};
		
		return View("CustomerForm",viewModel);
	}
	
	[HttpPost]
	public async Task<ActionResult> Save(CustomerFormViewModel viewModel)
	{
		if(!ModelState.IsValid)
		{
			var viewModelRetry = new CustomerFormViewModel()
			{
				Name = viewModel.Name,
				BirthDay = viewModel.BirthDay,
				IsSubscribedToNewsletter = viewModel.IsSubscribedToNewsletter,
				MembershipTypeId = viewModel.MembershipTypeId,
				ListMembershipTypes = await _db.MembershipTypes.ToListAsync()
			};
			return View("CustomerForm", viewModelRetry);
		}
		
		var membershipType = await _db.MembershipTypes.FirstOrDefaultAsync(mbs => mbs.Id == viewModel.MembershipTypeId);
		if(membershipType == null)
		{
			return NotFound();
		}
		
		var customer = await _db.Customers.FirstOrDefaultAsync(cst => cst.Id == viewModel.Id);
		if( customer == null)
		{
			var newCustomer = new Customer()
			{
				Name = viewModel.Name,
				BirthDay = viewModel.BirthDay,
				IsSubscribedToNewsletter = viewModel.IsSubscribedToNewsletter,
				MemberShipType = membershipType
			};
			
			await _db.Customers.AddAsync(newCustomer);
		}
		else
		{
			customer.Name = viewModel.Name;
			customer.BirthDay = viewModel.BirthDay;
			customer.IsSubscribedToNewsletter = viewModel.IsSubscribedToNewsletter;
			customer.MemberShipType = membershipType;
		}
		
		await _db.SaveChangesAsync();
		
		return RedirectToAction("Index", "Customers");
	}
	
	[Route("Edit")]
	public async Task<ActionResult> Edit(Guid Id)
	{
		var customer = await _db.Customers.Include(cst => cst.MemberShipType).FirstOrDefaultAsync(cst => cst.Id == Id);
		if(customer == null)
		{
			return NotFound();
		}
		
		var viewModel = new CustomerFormViewModel()
		{
			Name = customer.Name,
			BirthDay = customer.BirthDay,
			IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter,
			MembershipTypeId = customer.MemberShipType.Id,
			ListMembershipTypes = await _db.MembershipTypes.ToListAsync()
		};
		
		return View("CustomerForm", viewModel);
	}
}