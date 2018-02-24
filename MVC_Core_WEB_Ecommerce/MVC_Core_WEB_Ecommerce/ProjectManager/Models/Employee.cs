using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    public class Employee : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Cognome { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string Genere { get ; set; }

        public int ProgettoId { get; set; }
        public Project Progetto { get; set; }

        public string GetGenere(string _wordGenere)
        {

            if (_wordGenere == "M")
            {
                return "Maschio";
            }
            else if (_wordGenere == "F")

                return "Femmina";
            else
            {
                return "NonDichiarato";
            }
        }
    }
}
//[Id]
//      ,[Nome]
//      ,[Cognome]
//      ,[Genere]
//      ,[ProgettoId]
//FROM[dbo].[Employees]


 