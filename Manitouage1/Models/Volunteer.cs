using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manitouage1.Models
{
    public class Volunteer
    {
        [Key]
        public int volunteerId { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public bool policeCheckPass { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string phone { get; set; }

        [Required]
        public bool approved { get; set; }

        [AllowHtml]
        [Required]
        public string survey { get; set; }

        [ForeignKey("Event")]
        public int? EventId { get; set; }
        public Event Event { get; set; }

    }

    public class VolunteerDto
    {
        [DisplayName("Volunteer ID")]
        public int volunteerId { get; set; }

        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Police Check Pass")]
        public bool policeCheckPass { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        [DisplayName("Approved")]
        public bool approved { get; set; }

        [DisplayName("EventId")]
        public int? EventId { get; set; }


    }
}