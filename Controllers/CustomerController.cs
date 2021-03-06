using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
	public class CustomerController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Customer
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult AddCustomerDetails(string userId)
		{
			ViewBag.PreviousURL = Request.UrlReferrer;

			if (userId == null)
				return View();

			ApplicationUser user = db.Users.Find(userId);
			Customer customer = db.Customers
				.Where(m => m.EmailAddress == user.Email).FirstOrDefault();
			Session["UserEmail"] = user.Email;
			if (customer == null)
				// If theres no user with that email address, create new
				return View();

			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddCustomerDetails([Bind(
			Include = "ID,FirstName,LastName,BillingAddress,BillingZip,BillingCity," +
			"DeliveryAddress,DeliveryZip,DeliveryCity,EmailAddress,PhoneNo")] Customer customer, Uri previousUrl )
		{
			if (ModelState.IsValid)
			{
				if (customer.ID <= 0)
					db.Customers.Add(customer);
				else
					db.Entry(customer).State = EntityState.Modified;

				db.SaveChanges();

				// TODO: Change redirect to view customer details?
				return Redirect(previousUrl.ToString());
			}

			return View(customer);
		}

		[ChildActionOnly]
		public ActionResult CustomerDetailsPartial(string userId)
		{
			if (userId == null)
				return PartialView();

			ApplicationUser user = db.Users.Find(userId);
			Customer customer = db.Customers
				.Where(m => m.EmailAddress == user.Email).FirstOrDefault();
			if (customer == null)
				// If theres no user with that email address, create new
				return PartialView();

			return PartialView(customer);
		}
	}
}
