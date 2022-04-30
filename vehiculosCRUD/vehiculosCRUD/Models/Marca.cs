using System;
using System.Collections.Generic;

namespace vehiculosCRUD.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
