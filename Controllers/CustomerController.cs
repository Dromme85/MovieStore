using MovieStore.Models;
using System;
using System.Collections.Generic;
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

		public ActionResult AddCustomerDetails(int? customerid)
		{
			if (customerid == null)
				return View();

			Customer customer = db.Customers.Find(customerid);
			if (customer == null)
				return HttpNotFound();

			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddCustomerDetails([Bind(
			Include = "ID,FirstName,LastName,BillingAddress,BillingZip,BillingCity," +
			"DeliveryAddress,DeliveryZip,DeliveryCity,EmailAddress,PhoneNo")] Customer customer )
		{
			if (ModelState.IsValid)
			{
				//if (customer.UserID == 0) customer.UserID = 0;
				db.Customers.Add(customer);
				db.SaveChanges();
				// TODO: Change redirect to view customer details?
				return RedirectToAction("Index", "Home");
			}

			return View(customer);
		}
	}
}