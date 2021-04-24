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
    public class Department
    {
        [Key]
        public int departmentId { get; set; }

        [Required]
        public string departmentName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string phone { get; set; }

        
        public string fax { get; set; }

        [Required]
        public string extension { get; set; }

        [Required]
        public string category { get; set; }

        public ICollection<JobPosting> jobpostings { get; set; }


    }

    public class DepartmentDto
    {
        [DisplayName("Department ID")]
        public int departmentId { get; set; }

        [DisplayName("Department Name")]
        [Required(ErrorMessage = "Please Enter Department Name")]
        public string departmentName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please Enter Phone")]
        public string phone { get; set; }

        [DisplayName("Fax")]
        [Required(ErrorMessage = "Please Enter Fax")]
        public string fax { get; set; }

        [DisplayName("Extension")]
        [Required(ErrorMessage = "Please Enter Extension")]
        public string extension { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please Enter Category")]
        public string category { get; set; }

    }
}