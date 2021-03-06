using Microsoft.AspNet.Identity;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
	public class OrderController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Order
		public ActionResult Index()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			List<OrderVM> ovm = new List<OrderVM>();
			List<Order> orders = new List<Order>();

			if (User.Identity.IsAuthenticated)
			{
				string uEmail = db.Users.Find(User.Identity.GetUserId()).Email;
				orders = db.Orders
					.Where(o => o.Customer.EmailAddress == uEmail).ToList();
			}
			// if no user is logged in, just fetch all orders
			else orders = db.Orders.ToList();


			if (orders != null)
			{
				foreach (var item in orders)
				{
					item.Customer = db.Customers.Find(item.CustomerID);

					var orl = db.OrderRows.Where(m => m.OrderID == item.ID).ToList();

					foreach (var or in orl)
					{
						or.Movie = db.Movies.Find(or.MovieID);
					}

					ovm.Add(new OrderVM() { Order = item, OrderRows = orl });
				}
			}

			return View(ovm);
		}

		public ActionResult ViewCart()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			return View();
		}

		public ActionResult PlaceOrder()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			ViewBag.OrderMessage = "";
			ViewBag.OrderStatus = false;

			return View();
		}

		public ActionResult ConfirmOrder(string userId)
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			if (userId == null)
			{
				ViewBag.OrderMessage = "Something went wrong, cant find user";
				ViewBag.OrderStatus = false;
				return View();
			}

			ApplicationUser user = db.Users.Find(userId);
			Customer customer = db.Customers
				.Where(m => m.EmailAddress == user.Email).FirstOrDefault();
			if (customer == null)
			{
				ViewBag.OrderMessage = "Something went wrong, cant find customer";
				ViewBag.OrderStatus = false;
				return View();
			}

			Order order = new Order()
			{
				OrderDate = DateTime.Now,
				Customer = customer,
				CustomerID = customer.ID,
			};

			db.Orders.Add(order);
			db.SaveChanges();

			order = db.Orders.OrderByDescending(m => m.ID).FirstOrDefault();

			var cartItems = (List<CartViewModel>)Session["CartItems"];

			foreach (var item in cartItems)
			{
				for (int i = 0; i < item.Amount; i++)
				{
					var orow = new OrderRow()
					{
						OrderID = order.ID,
						MovieID = item.Movie.ID,
						Price = item.Movie.Price
					};

					db.OrderRows.Add(orow);
					db.SaveChanges();
				}
			}

			// If you get here, the order should be done and confirmed
			ViewBag.OrderMessage = "Order Confirmed!";
			ViewBag.OrderStatus = true;

			// Clear the cart
			cartItems.Clear();
			Session["CartItems"] = cartItems;

			return View();
		}
	}
}
