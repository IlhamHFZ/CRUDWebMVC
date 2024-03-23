using CRUDWebMVC.Presistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebMVC.Controllers;

[Route("Movies")]
public class MoviesController : Controller
{
	private readonly Database _db;
	public MoviesController(Database db)
	{
		_db = db;
	}
	
	[Route("Index")]
	public async Task<ActionResult> Index()
	{
		var movies = await _db.Movies.Include(mvo => mvo.Genre).OrderBy(cst => cst.Name).ToListAsync();
		return View(movies);
	}
	
	[Route("Detail/{Id:Guid}")]
	public async Task<ActionResult> Detail([FromRoute] Guid Id)
	{
		var movie = await _db.Movies.Include(mvo => mvo.Genre).FirstOrDefaultAsync(mvo => mvo.Id == Id);
		if(movie == null)
		{
			return NotFound();
		}
		return View(movie);
	}
}