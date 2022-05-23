using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vehiculosCRUD.Models
{
    public partial class Vehiculo
    {
        [Required]
        [StringLength(8)]
        public string Patente { get; set; } = null!;
        [Required]
        public int IdPropietario { get; set; }
        [Required]
        public int IdMarca { get; set; }
        [Required]
        public string Modelo { get; set; } = null!;
        [Range(2, 4)]
        public int? CantPuertas { get; set; }

        public virtual Marca? Marca { get; set; } = null!;
        public virtual Propietario? Propietario { get; set; } = null!;
    }
}
