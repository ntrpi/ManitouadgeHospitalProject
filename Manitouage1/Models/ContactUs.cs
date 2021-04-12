using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manitouage1.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactUsId
        {
            get; set;
        }
        [Required]
        public string FirstName
        {
            get; set;
        }
        [Required]
        public string LastName
        {
            get; set;
        }
        [Required]
        public string Email
        {
            get; set;
        }
        [Required]
        public string Message
        {
            get; set;
        }
        [Required]
        public string Reply
        {
            get; set;
        }
        [Required]
        public string Status
        {
            get; set;
        }


    }
    public class ContactUsDto
    {
        [DisplayName("ContactUs ID")]
        public int ContactUsId { get; set; }

        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [DisplayName("LastName")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        [DisplayName("Reply")]
        public string Reply { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}