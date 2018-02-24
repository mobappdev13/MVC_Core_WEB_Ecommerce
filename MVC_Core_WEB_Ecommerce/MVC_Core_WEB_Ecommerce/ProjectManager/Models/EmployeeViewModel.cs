using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManager.Models
{
    public class EmployeeViewModel
    {
        public Employee Employee{ get; set; }
        public IEnumerable<SelectListItem> ProjectsList { get; set; }
    }
}
