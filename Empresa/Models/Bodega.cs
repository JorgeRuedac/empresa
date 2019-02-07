using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Empresa.Models
{
    [Table("Bodega", Schema="")]
    public class Bodega
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [StringLength(10)]
        [Display(Name = "Bodega I D")]
        public String BodegaID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Productos I D")]
        public String ProductosID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Observacion")]
        public String Observacion { get; set; }


    }
}
 
