using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    //view model required to add a donation for events if need
    public class AddDonation
    {
        //Information about the donation
        public DonationDto donation { get; set; }
        //Needed for a dropdownlist which will let users add donation to a specfic event
        public IEnumerable<EventDto> allevents { get; set; }
    }
}