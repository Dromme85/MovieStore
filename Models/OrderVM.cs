using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
	public class OrderVM
	{
		public Order Order { get; set; }
		public List<OrderRow> OrderRows { get; set; }
	}

	public class OrderSumVM
	{
		public Customer Customer { get; set; }
		public decimal Total { get; set; }
	}
}