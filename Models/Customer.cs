using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
	public class Customer
	{
		public int ID { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters. ")]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters. ")]
		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Billing Address cannot be longer than 50 characters. ")]
		[DisplayName("Billing Address")]
		public string BillingAddress { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		[DisplayName("Billing Zip Code")]
		public string BillingZip { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Billing City cannot be longer than 50 characters. ")]
		[DisplayName("Billing City")]
		public string BillingCity { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Delivery Address cannot be longer than 50 characters. ")]
		[DisplayName("Delivery Address")]
		public string DeliveryAddress { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		[DisplayName("Delivery Zip Code")]
		public string DeliveryZip { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "Delivery City cannot be longer than 50 characters. ")]
		[DisplayName("Delivery City")]
		public string DeliveryCity { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[DisplayName("E-mail Address")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[DisplayName("Phone Number")]
		public string PhoneNo { get; set; }

		//[Required]
		//[StringLength(128)]
		//public string UserID { get; set; }		
	}
}