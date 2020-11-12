using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace MovieStore.Models
{
	public class Movie
	{
		public int ID { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Movie Title cannot be longer than 100 characters. ")]
		[DisplayName("Movie Title")]
		public string Title { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Director cannot be longer than 100 characters. ")]
		public string Director { get; set; }

		[Required]
		[Range(1900, 2100, ErrorMessage = "Release Year must be between 1900 and 2100. ")]
		[DisplayName("Year")]
		public int ReleaseYear { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		/// <summary>
		/// Not required, prefere image from themoviedb.org (prefered size 1000x1500px)
		/// </summary>
		[DataType(DataType.ImageUrl)]
		[DisplayName("Image URL")]
		public string ImageURL { get; set; }
	}
}