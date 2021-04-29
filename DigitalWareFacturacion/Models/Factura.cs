using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public long PkIdFactura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public long UltimoModificador { get; set; }
        public long FkIdCliente { get; set; }
        public long FkIdTipoPago { get; set; }

        public virtual Cliente FkIdClienteNavigation { get; set; }
        public virtual TipoPago FkIdTipoPagoNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
