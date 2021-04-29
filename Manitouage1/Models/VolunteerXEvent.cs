using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Manitouage1.Models
{
    public class VolunteerXEvent
    {

        public int volunteerId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public bool policeCheckPass { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public bool approved { get; set; }


        public int EventId { get; set; }

        public string Title { get; set; }

    }
}