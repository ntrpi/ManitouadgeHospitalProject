using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouadge.Models.ViewModels
{
    public class ListDonation
    {

        public IEnumerable<DonationDto> alldonations { get; set; }

        public EventDto Event { get; set; }

    }
}