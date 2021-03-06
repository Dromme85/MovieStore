using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
	public class MovieController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		List<CartViewModel> cartItems = new List<CartViewModel>();
		// GET: Movie
		public ActionResult Index(int? page, int? length)
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			var movies = db.Movies.ToList();

			if (length == null) length = 6;
			if (page == null) page = 0;

			if (movies.Count < length * page) page--;

			ViewBag.Length = length;
			ViewBag.Page = page;
			ViewBag.MaxPage = (movies.Count / length) - (movies.Count % length == 0 ? 1 : 0);

			movies = movies.Skip((int)(page * length)).Take((int)length).ToList();
			
			return View(movies);
		}

		public ActionResult Details(int? movieid)
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			if (movieid == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Movie movie = db.Movies.Find(movieid);
			if (movie == null)
				return HttpNotFound();

			return View(movie);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,Title,Director,ReleaseYear,Price,ImageURL")] Movie movie)
		{
			if (ModelState.IsValid)
			{
				db.Movies.Add(movie);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(movie);
		}

		public ActionResult Edit(int? movieid)
		{
			if (movieid == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Movie movie = db.Movies.Find(movieid);
			if (movie == null)
				return HttpNotFound();

			return View(movie);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Title,Director,ReleaseYear,Price,ImageURL")] Movie movie)
		{
			if (ModelState.IsValid)
			{
				db.Entry(movie).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Details", new { MovieId = movie.ID });
			}
			return View(movie);
		}

		public ActionResult AddToCart([Bind(Prefix = "movieID")] int movieID)
		{
			var movie = db.Movies.SingleOrDefault(m => m.ID == movieID);

			if (Session.IsNewSession && cartItems != null ||
				Session["CartItems"] == null && cartItems != null)
			{
				cartItems.Add(new CartViewModel(1, movie));
				Session["CartItems"] = cartItems;
			}
			else if (Session["CartItems"] != null)
			{
				cartItems = (List<CartViewModel>)Session["CartItems"];
				int index = cartItems.FindIndex(m => m.Movie.ID == movieID);

				if (index != -1)
					cartItems[index].Amount++;

				else
					cartItems.Add(new CartViewModel(1, movie));

				Session["CartItems"] = cartItems;
			}

			return RedirectToAction("Index", "Movie");
		}

		// TODO: Combine first two if statements if return statements still commented
		public void RemoveFromCart([Bind(Prefix = "movieID")] int movieID)
		{
			if (Session.Count > 0 && Session["CartItems"] != null)
			{
				if (cartItems != null)
				{
					cartItems = (List<CartViewModel>)Session["CartItems"];
					int index = cartItems.FindIndex(m => m.Movie.ID == movieID);

					if (cartItems[index].Amount > 1)
						cartItems[index].Amount--;

					else
						cartItems.Remove(cartItems.First(m => m.Movie.ID == movieID));

					Session["CartItems"] = cartItems;
				}
				//else return RedirectToAction("Index", "Movie");
			}

			//return RedirectToAction("ViewCart", "Order");
		}
	}
}
