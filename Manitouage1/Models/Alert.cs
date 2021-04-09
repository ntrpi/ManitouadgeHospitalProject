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
        public ICollection<JobPosting> jobpostings { get; set; }

        public ICollection<Event> events { get; set; }

    }
    public class AlertDto
    {
        [DisplayName("Alert ID")]
        public int alertId { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Date and Time")]
        public DateTime dateTime { get; set; }

        [DisplayName("Description")]
        public  string description { get; set; }

        public int? jobPostingId { get; set; }

        public int? EventId { get; set; }

    }

}