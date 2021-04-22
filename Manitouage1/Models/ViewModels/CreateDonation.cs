using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class CreateDonation
    {
            public DonationDto donation { get; set; }

            public IEnumerable<EventDto> allevents { get; set; }
        
    }
}
