using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Empresa.Models
{
    [Table("Cliente", Schema="")]
    public class Cliente
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cliente I D")]
        public Int32 Cliente_ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Apellido")]
        public String Apellido { get; set; }

        [StringLength(50)]
        [Display(Name = "Direccion")]
        public String Direccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Telefono")]
        public String Telefono { get; set; }

        [StringLength(50)]
        [Display(Name = "Correo")]
        public String Correo { get; set; }


    }
}
 
