using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouadge.Models.ViewModels
{
    public class CreateDonation
    {
            public DonationDto donation { get; set; }

            public IEnumerable<EventDto> allevents { get; set; }
        
    }
}
