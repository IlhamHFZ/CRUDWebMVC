using CRUDWebMVC.Models.ViewModel;
using CRUDWebMVC.Models.Entity;
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
	
	[Route("Create")]
	public async Task<ActionResult> Create()
	{
		var genres = await _db.Genres.ToListAsync();
		var viewModel = new MovieFormViewModel()
		{
			Genres = genres
		};
		
		return View("MovieForm", viewModel);
	}
	
	[Route("Edit")]
	public async Task<ActionResult> Edit(Guid Id)
	{
		var movie = await _db.Movies.Include(mvo => mvo.Genre).FirstOrDefaultAsync(mvo => mvo.Id == Id);
		if(movie == null)
		{
			return NotFound();
		}
		
		var viewModel = new MovieFormViewModel()
		{
			Name = movie.Name,
			ReleaseDate = movie.ReleaseDate,
			DateAdded = movie.DateAdded,
			Stock = movie.Stock,
			GenreId = movie.Genre.Id,
			Genres = await _db.Genres.ToListAsync()
		};
		
		return View("MovieForm", viewModel);
	}
	
	[HttpPost]
	public async Task<ActionResult> Save(MovieFormViewModel viewModel)
	{
		var genre = await _db.Genres.FirstOrDefaultAsync(gnr => gnr.Id == viewModel.GenreId);
		if(genre == null)
		{
			return NotFound();
		}
		
		var movie = await _db.Movies.FirstOrDefaultAsync();
		if(movie == null)
		{
			var newMovie = new Movie()
			{
				Name = viewModel.Name,
				ReleaseDate = viewModel.ReleaseDate,
				DateAdded = viewModel.DateAdded,
				Stock = viewModel.Stock,
				Genre = genre
			};
			await _db.Movies.AddAsync(newMovie);
		}
		else
		{
			movie.Name = viewModel.Name;
			movie.ReleaseDate = viewModel.ReleaseDate;
			movie.DateAdded = viewModel.DateAdded;
			movie.Stock = viewModel.Stock;
			movie.Genre = genre;
		}
		
		await _db.SaveChangesAsync();
		
		return RedirectToAction("Index", "Movies");
	}
	
	public async Task<ActionResult> Delete(Guid Id)
	{
		var movie = await _db.Movies.FirstOrDefaultAsync(mvo => mvo.Id == Id);
		if(movie == null)
		{
			return NotFound();
		}
		
		_db.Movies.Remove(movie);
		await _db.SaveChangesAsync();
		
		return RedirectToAction("Index", "Movies");
	}
}