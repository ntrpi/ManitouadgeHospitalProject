using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

    }

    public class DepartmentDto
    {
        [DisplayName("Department ID")]
        public int departmentId { get; set; }

        [DisplayName("Department Name")]
        public string departmentName { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        [DisplayName("Faz")]
        public string fax { get; set; }

        [DisplayName("Extension")]
        public string extension { get; set; }

        [DisplayName("Category")]
        public string category { get; set; }

    }
}