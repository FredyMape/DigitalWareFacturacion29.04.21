using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class TipoPago
    {
        public TipoPago()
        {
            Facturas = new HashSet<Factura>();
        }

        public long PkIdTipoPago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public long UltimoModificador { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
