using System;
using System.Collections.Generic;

namespace vehiculosCRUD.Model
{
    public partial class Vehiculo
    {
        public string Patente { get; set; } = null!;
        public int IdPropietario { get; set; }
        public int IdMarca { get; set; }
        public string Modelo { get; set; } = null!;
        public int? CantPuertas { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; } = null!;
        public virtual Propietario IdPropietarioNavigation { get; set; } = null!;
    }
}
