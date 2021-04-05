using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class CreateJobPosting
    {
        public JobPostingDto jobposting { get; set; }

        public IEnumerable<DepartmentDto> alldepartments { get; set; }
    }
}