using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Manitouage1.Models
{
    public class Testimonial
    {
        [Key]
        public int testimonialId { get; set; }

        [Required]
        public DateTime creationDate { get; set; }

        [Required]
        public string testimonial { get; set; }

        [Required]
        public bool approved { get; set; }

        public string UserId { get; set; }


    }

    public class TestimonialDto
    {
        [DisplayName("Testimonial ID")]
        public int testimonialId { get; set; }

        [DisplayName("Creation Date")]
        public DateTime creationDate { get; set; }

        [DisplayName("Testimonial")]
        public string testimonial { get; set; }

        [DisplayName("Approved")]
        public bool approved { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

    }
}