using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ShowDepartment
    {
        public DepartmentDto department { get; set; }

        public JobPostingDto jobPosting { get; set; }
    }
}