using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Empresa.Models
{
    [Table("productos", Schema="")]
    public class productos
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Productos I D")]
        public Int32 ProductosID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Producto")]
        public String Nombre_producto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Precio Unitario")]
        public String Precio_unitario { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Precio Venta Publico")]
        public String Precio_venta_publico { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Fecha Fabricacion")]
        public String Fecha_fabricacion { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Fecha Caducidad")]
        public String Fecha_caducidad { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Iva")]
        public String Iva { get; set; }


    }
}
 
