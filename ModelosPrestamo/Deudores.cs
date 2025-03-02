using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelosPrestamo;

public partial class Deudores
{
    [Key]
    public int DeudorId { get; set; }

    public string Nombres { get; set; } = null!;

    [InverseProperty("Deudor")]
    public virtual ICollection<Cobros> Cobros { get; set; } = new List<Cobros>();

    [InverseProperty("Deudor")]
    public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
    /*
     [InverseProperty("Deudor")]: This attribute specifies the inverse navigation property in the Cobros class. 
            It tells EF that the Cobros collection in the Deudores class corresponds to the Deudor property in the Cobros class.
       public virtual ICollection<Cobros> Cobros { get; set; } = new List<Cobros>();: This is a navigation property that represents a collection of Cobros entities related to the Deudores entity. 
            The virtual keyword enables lazy loading, and the property is initialized to an empty list.
     */
}
