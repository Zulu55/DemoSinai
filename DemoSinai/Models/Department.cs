namespace DemoSinai.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Departamentos")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} puede tener máximo {1} carateres de longitud.")]
        [Index("Department_Name_Index", IsUnique = true)]
        [Column("Nombre")]
        public string Name { get; set; }
    }
}