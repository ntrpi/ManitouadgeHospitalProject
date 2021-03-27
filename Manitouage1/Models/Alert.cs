using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manitouage1.Models
{
    public class Alert
    { 
        [Key]
        public int alertId
        {
            get; set;
        }
        [Required]
        public string title
        {
            get; set;
        }
        [Required]
        public DateTime dateTime
        {
            get; set;
        }
        [Required]
        public string description
        {
            get; set;
        }
        [ForeignKey("JobPosting")]
        public int jobPostingId 
        { 
            get; set; 
        }
        public virtual JobPosting JobPosting
        {
            get; set;
        }
        [ForeignKey("Event")]
        public int EventId
        {
            get; set;
        }
        public virtual Event Event
        {
            get; set;
        }

    }
}