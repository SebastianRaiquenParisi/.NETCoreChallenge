using System;
using System.Collections.Generic;

namespace vehiculosCRUD.Models
{
    public partial class Vehiculo
    {
        public string Patente { get; set; } = null!;
        public int IdPropietario { get; set; }
        public int IdMarca { get; set; }
        public string Modelo { get; set; } = null!;
        public int? CantPuertas { get; set; }

        public virtual Marca Marca { get; set; } = null!;
        public virtual Propietario Propietario { get; set; } = null!;
    }
}
