using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouadge.Models.ViewModels
{
    public class CreateJobPosting
    {
        public JobPostingDto jobposting { get; set; }

        public IEnumerable<DepartmentDto> alldepartments { get; set; }
    }
}