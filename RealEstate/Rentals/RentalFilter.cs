using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Rentals
{
    public class RentalFilter
    {

        public enum SortOptions
        {
            [Display(Name = "Rental")]
            Rental,
            [Display(Name = "Price")]
            Price
        };

        public decimal? PriceLimit { get; set; }
        public SortOptions SortBy { get; set; }
    }
}