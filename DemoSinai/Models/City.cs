using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoSinai.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        [Index("City_Name_DepartmentId_Index", IsUnique = true, Order = 1)]
        public string Name { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Index("City_Name_DepartmentId_Index", IsUnique = true, Order = 2)]
        public int DepartmentId { get; set; }

        public int CityCode { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}