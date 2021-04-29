using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouadge.Models.ViewModels
{
    public class ShowDepartment
    {
        public DepartmentDto department { get; set; }

        public JobPostingDto jobPosting { get; set; }
    }
}