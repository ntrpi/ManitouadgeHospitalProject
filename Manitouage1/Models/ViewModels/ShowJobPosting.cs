using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ShowJobPosting
    {
        public bool isadmin { get; set; }

        public JobPostingDto jobposting { get; set; }

        public DepartmentDto department { get; set; }
    }
}