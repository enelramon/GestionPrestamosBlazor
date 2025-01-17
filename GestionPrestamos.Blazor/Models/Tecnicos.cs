using System.ComponentModel.DataAnnotations;

namespace GestionPrestamos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string Nombres { get; set; } = null!;

    [Range(1, double.MaxValue, ErrorMessage = "El sueldo no puede ser menor a 1")]
    public double SueldoHora { get; set; }
}