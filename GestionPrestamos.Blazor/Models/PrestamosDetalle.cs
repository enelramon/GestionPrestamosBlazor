using GestionPrestamos.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PrestamosDetalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El número de cuota es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El número de cuota debe ser mayor a 0.")]
    public int CuotaNo { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El valor es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0.")]
    public double Valor { get; set; }

  
    public double Balance { get; set; }

    [Required(ErrorMessage = "El ID del préstamo es obligatorio.")]
    public int PrestamoId { get; set; }

    [ForeignKey("PrestamoId")]
    public virtual Prestamos Prestamo { get; set; }


}