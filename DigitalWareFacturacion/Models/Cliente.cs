using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public long PkIdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimineto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public long UltimoModificador { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
