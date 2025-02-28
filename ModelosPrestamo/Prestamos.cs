using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelosPrestamo;

public partial class Prestamos
{
    [Key]
    public int PrestamoId { get; set; } // No es requerido porque es autoincremental

    [Required(ErrorMessage = "El campo Concepto es obligatorio.")]
    public string Concepto { get; set; } = null!;

    [Required(ErrorMessage = "El campo Monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor a 0.")]
    public double Monto { get; set; }

   
    public double Balance { get; set; }

    [Required(ErrorMessage = "El campo DeudorId es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un deudor válido.")]
    public int DeudorId { get; set; }

    [ForeignKey("DeudorId")]
    [InverseProperty("Prestamos")]
    public virtual Deudores Deudor { get; set; } = null!;

    [InverseProperty("Prestamo")]
    public virtual ICollection<PrestamosDetalle> PrestamosDetalle { get; set; } = new List<PrestamosDetalle>();

}