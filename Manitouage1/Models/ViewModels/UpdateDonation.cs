using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class UpdateDonation
    {
        //could have used create donation although the naming conventiopn for view model might not have been good 
        public DonationDto donation { get; set; }

        public IEnumerable<EventDto> allevents { get; set; }
    }
}