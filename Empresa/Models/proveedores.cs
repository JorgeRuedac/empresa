using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Empresa.Models
{
    [Table("proveedores", Schema="")]
    public class proveedores
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Proveedores I D")]
        public Int32 ProveedoresID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Empresa")]
        public String Nombre_empresa { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Direccion")]
        public String Direccion { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Telefono")]
        public String Telefono { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Celular Contacto")]
        public String Celular_contacto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Contacto")]
        public String Nombre_contacto { get; set; }


    }
}
 
