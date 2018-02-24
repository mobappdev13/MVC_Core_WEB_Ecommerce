using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectManager.Models
{
    public class Project : BaseModel
    {
        [Required]
        [MaxLength(5)]
        public string Codice { get; set; }
        [Required]
        [MaxLength(50)]
        public string Tecnologia { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DataScadenza { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DataConsegnaEffettiva { get; set; }
        [Range(0, 1000)]
        public int DipendentiRichiesti { get; set; }

        //for view employees list
        public IEnumerable<Employee> ListEmployeesOnProject { get; set; }

        //Methods for Project Time Control 
        public string GetOnTimeProject(DateTime _dataScadenza, DateTime? _dataConsegnaEffettiva)
        {

            if (_dataConsegnaEffettiva > _dataScadenza)
            {
                return "No";
            }
            else
            {
                return "Si";
            }
        }

    }
}
