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
		public ActionResult Index()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
			
			return View(db.Movies.ToList());
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

		public ActionResult AddToCart([Bind(Prefix = "movieID")] int movieID, string Ctr, string Act)
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

			if (Ctr == null)
				return RedirectToAction("Index", "Movie");
			else
				return RedirectToAction(Ctr, Act);
		}

		public ActionResult RemoveFromCart([Bind(Prefix = "movieID")] int movieID)
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
				else return RedirectToAction("Index", "Movie");
			}

			return RedirectToAction("ViewCart", "Order");
		}
	}
}