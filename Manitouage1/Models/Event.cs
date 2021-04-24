using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manitouage1.Models
{
    public class Event
    {
        [Key]
        public int EventId
        {
            get; set;
        }
        [Required]
        public string Title
        {
            get; set;
        }
        [Required]
        public string Description
        {
            get; set;
        }
        [Required]
        public DateTime DateTime
        {
            get; set;
        }
        [Required]
        public string Location
        {
            get; set;
        }
        [Required]
        public Decimal Duration
        {
            get; set;
        }
        [Required]
        public string ContactPerson
        {
            get; set;
        }
  
        //Wafa: an event can have many donations
        public ICollection<Donation> Donations { get; set; }
    }
    public class EventDto
    {
        [DisplayName("Event ID")]
        public int EventId { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Event Title")]
        public string Title { get; set; }

        [DisplayName("Date and Time")]
        public DateTime DateTime { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Duration")]
        public Decimal Duration { get; set; }

        [DisplayName("Contact Personnel")]
        public string ContactPerson { get; set; }

        public int NumDonations { get; set; }

    }
}