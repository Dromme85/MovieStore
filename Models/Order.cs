using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
	public class Order
	{
		public int ID { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayName("Order Date")]
		public DateTime OrderDate { get; set; }

		public int CustomerID { get; set; }
		public Customer Customer { get; set; }
	}
}