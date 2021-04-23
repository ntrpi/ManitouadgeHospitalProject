using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Manitouage1.Models
{
    public class Donation
    {
        [Key]
        public int donationId { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

        public int phoneNumber { get; set; }


        public decimal amount { get; set; }

        //a donation can be made to one event
        [ForeignKey("Event")]
        //if it's null
        public int? EventId { get; set; }
        public virtual Event Event { get; set; }


    }

    public class DonationDto
    {
        [DisplayName("Donation ID")]
        public int donationId { get; set; }

        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public int phoneNumber { get; set; }

        [DisplayName("Amount")]
        public decimal amount { get; set; }

        [DisplayName("Event Id")]
        public int? EventId { get; set; }

        [DisplayName("Event Name")]
        public string Title { get; set; }


    }
}