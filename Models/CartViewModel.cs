using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
	public class CartViewModel
	{
		public int Amount { get; set; }
		public Movie Movie { get; set; }

		public CartViewModel(int a, Movie m) { Amount = a; Movie = m; }

		public int Add()
		{
			return Amount++;
		}

		public int Sub()
		{
			return Amount--;
		}
	}
}