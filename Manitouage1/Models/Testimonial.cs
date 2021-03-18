using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }

    public class TestimonialDto
    {
        [DisplayName("Testimonial ID")]
        public int testimonialId { get; set; }

        [DisplayName("Creation Date")]
        public DateTime creationDate { get; set; }

        [DisplayName("Testimonial")]
        public string testimonial { get; set; }

        [DisplayName("User Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}