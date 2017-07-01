namespace DemoSinai.Models
{
    using System.ComponentModel.DataAnnotations;

    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        public string Address { get; set; }
    }
}