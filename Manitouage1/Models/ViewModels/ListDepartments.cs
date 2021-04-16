using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ListDepartments
    {
        public bool isadmin { get; set; }

        public IEnumerable<DepartmentDto> departments { get; set; }
    }
}