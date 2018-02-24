using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManager.Models
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }

        public IEnumerable<SelectListItem> technologiesList { get; set; }
    }
}
