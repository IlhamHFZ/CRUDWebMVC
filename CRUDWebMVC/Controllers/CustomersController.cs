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
	
}