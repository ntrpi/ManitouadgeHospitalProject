using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ViewDonation
    {

        public IEnumerable<DonationDto> donation { get; set; }

        public EventDto EventId { get; set; }

    }
}