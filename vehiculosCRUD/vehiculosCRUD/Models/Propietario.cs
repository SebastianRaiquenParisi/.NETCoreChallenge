using System;
using System.Collections.Generic;

namespace vehiculosCRUD.Models
{
    public partial class Propietario
    {
        public Propietario()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;


        public virtual ICollection<Vehiculo> Vehiculos { get; set; }

    }
}
