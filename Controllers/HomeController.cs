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

			var popMovies = db.OrderRows
				.GroupBy(or => or.Movie)
				.OrderByDescending(or => or.Count())
				.Select(or => or.Key)
				.Take(6).ToList();
			movieLists.Add(popMovies);

			var newestMovies = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(6).ToList();
			movieLists.Add(newestMovies);

			var oldestMovies = db.Movies.OrderBy(m => m.ReleaseYear).Take(6).ToList();
			movieLists.Add(oldestMovies);

			var cheapestMovies = db.Movies.OrderBy(m => m.Price).Take(6).ToList();
			movieLists.Add(cheapestMovies);

			var bestCustomer = db.OrderRows
				.GroupBy(or => or.Order)
				.Select(g => new OrderSumVM
				{
					Customer = g.Key.Customer,
					Total = g.Sum(or => or.Price)
				})
				.OrderByDescending(g => g.Total).First();
			ViewBag.BestCust = bestCustomer;

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