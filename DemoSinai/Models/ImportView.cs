namespace DemoSinai.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ImportView
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Archivo de Departamentos")]
        public HttpPostedFileBase DeparmentsFile { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Archivo de Ciudades")]
        public HttpPostedFileBase CitiesFile { get; set; }
    }
}