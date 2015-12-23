using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Rentals
{
    public class RentalsList
    {
        public IEnumerable<Rental> Rentals { get; set; }
        public RentalFilter RentalFilter { get; set; }

    }
}