using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Manitouage1.Models
{
    public class JobPosting
    {
        [Key]
        public int jobPostingId { get; set; }

        [Required]
        public string jobNumber { get; set; }

        [Required]
        public string jobTitle { get; set; }

        [Required]
        public string jobType { get; set; }

        [Required]
        public string jobDescription { get; set; }

        [Required]
        public string salary { get; set; }

        [Required]
        public DateTime closingDate { get; set; }

        [ForeignKey("Department")]
        public int departmentId { get; set; }
        public string departmentName { get; set; }

        public virtual Department Department { get; set; }

       // public ICollection<Department> Hairstyles { get; set; }

    }

    public class JobPostingDto
    {
        [DisplayName("Job Posting ID")]
        public int jobPostingId { get; set; }

        [DisplayName("Job Number")]
        public string jobNumber { get; set; }

        [DisplayName("Job Title")]
        public string jobTitle { get; set; }

        [DisplayName("Job Type")]
        public string jobType { get; set; }

        [DisplayName("Job Description")]
        public string jobDescription { get; set; }

        [DisplayName("Salary")]
        public string salary { get; set; }

        [DisplayName("Closing Date")]
        public DateTime closingDate { get; set; }

        [DisplayName("Department ID")]
        public int departmentId { get; set; }

        [DisplayName("Department Name")]
        public string departmentName { get; set; }
    }
}