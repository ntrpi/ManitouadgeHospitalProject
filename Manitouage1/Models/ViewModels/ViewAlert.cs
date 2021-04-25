using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Manitouage1.Models.ViewModels
{
    public class ViewAlert
    {
        public AlertDto alert
        {
            get; set;
        }
        public EventDto eventDto
        {
            get; set;
        }
        public JobPostingDto jobPosting
        {
            get; set;
        }
    }
}