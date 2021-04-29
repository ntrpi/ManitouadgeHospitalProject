using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouadge.Models.ViewModels
{
    public class ListJobPostings
    {
        public bool isadmin { get; set; }

        public IEnumerable<JobPostingDto> jobpostings { get; set; }
    }
}