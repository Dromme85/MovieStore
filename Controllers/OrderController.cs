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
		// GET: Order
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ViewCart()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

			return View();
		}

		public ActionResult PlaceOrder()
		{
			return View();
		}
	}
}