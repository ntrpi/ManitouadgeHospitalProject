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

        [AllowHtml]
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

    }

    public class JobPostingDto
    {
        [DisplayName("Job Posting ID")]
        public int jobPostingId { get; set; }

        [DisplayName("Job Number")]
        [Required(ErrorMessage = "Please Enter Job Number")]
        public string jobNumber { get; set; }

        [DisplayName("Job Title")]
        [Required(ErrorMessage = "Please Enter Job Title")]
        public string jobTitle { get; set; }

        [DisplayName("Job Type")]
        [Required(ErrorMessage = "Please Enter Job Type")]
        public string jobType { get; set; }

        [DisplayName("Job Description")]
        [Required(ErrorMessage = "Please Enter Job Description")]
        public string jobDescription { get; set; }

        [DisplayName("Salary")]
        [Required(ErrorMessage = "Please Enter Salary")]
        public string salary { get; set; }

        [DisplayName("Closing Date and Time")]
        [Required(ErrorMessage = "Please Enter Closing Date and Time")]
        public DateTime closingDate { get; set; }

        [DisplayName("Department ID")]
        public int departmentId { get; set; }

        [DisplayName("Department Name")]
        public string departmentName { get; set; }
    }
}