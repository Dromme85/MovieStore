using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
	public class OrderRow
	{
		public int ID { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public int OrderID { get; set; }
		public Order Order { get; set; }

		public int MovieID { get; set; }
		public Movie Movie { get; set; }
	}
}