using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace vehiculosCRUD.Models
{
    public partial class Propietario
    {
        public Propietario()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Apellido { get; set; } = null!;


        public virtual ICollection<Vehiculo> Vehiculos { get; set; }

    }
}
