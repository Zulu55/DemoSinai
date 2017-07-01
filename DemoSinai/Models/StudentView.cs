namespace DemoSinai.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class StudentView : Student
    {
        [Display(Name = "Foto")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Departamento")]
        public int DeparmentId { get; set; }
    }
}