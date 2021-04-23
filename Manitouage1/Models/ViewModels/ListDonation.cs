using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ListDonation
    {

        public IEnumerable<DonationDto> alldonations { get; set; }

        public EventDto Event { get; set; }

    }
}