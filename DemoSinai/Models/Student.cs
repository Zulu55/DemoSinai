using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoSinai.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string LastName { get; set; }

        [Display(Name = "Colegio")]
        public int SchoolId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        [Index("Student_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Display(Name = "Foto")]
        public string Picture { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string Address { get; set; }

        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        public virtual School School { get; set; }

        public virtual City City { get; set; }
    }
}