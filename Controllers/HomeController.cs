using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		public ActionResult Index()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
			List<List<Movie>> movieLists = new List<List<Movie>>();

			var moviesPop = db.Movies.ToList();
			var popList = new List<CartViewModel>();
			foreach (var mov in moviesPop)
			{
				popList.Add(new CartViewModel(0, mov));
			}

			var orderRows = db.OrderRows.ToList();
			foreach (var or in orderRows)
			{
				var movie = popList.Where(m => m.Movie.ID == or.MovieID).First();
				movie.Amount++;
			}
			moviesPop = popList.OrderByDescending(m => m.Amount).Select(m => m.Movie).ToList();
			movieLists.Add(moviesPop);

			var newestMovies = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
			movieLists.Add(newestMovies);

			var oldestMovies = db.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
			movieLists.Add(oldestMovies);

			var cheapestMovies = db.Movies.OrderBy(m => m.Price).Take(5).ToList();
			movieLists.Add(cheapestMovies);

			return View(movieLists);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}