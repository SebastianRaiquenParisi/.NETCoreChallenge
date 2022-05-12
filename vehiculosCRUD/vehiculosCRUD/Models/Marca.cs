using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vehiculosCRUD.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
