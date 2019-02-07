using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Empresa.Models
{
    [Table("contador", Schema="")]
    public class contador
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Contador Id")]
        public Int32 contador_id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido")]
        public String apellido { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Direccion")]
        public String direccion { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Contacto")]
        public String contacto { get; set; }


    }
}
 
