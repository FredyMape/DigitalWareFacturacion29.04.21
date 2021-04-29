using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class DetalleFactura
    {
        public long PkIdDetalleFactura { get; set; }
        public byte Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public long UltimoModificador { get; set; }
        public long FkIdFactura { get; set; }
        public long FkIdProducto { get; set; }

        public virtual Factura FkIdFacturaNavigation { get; set; }
        public virtual Producto FkIdProductoNavigation { get; set; }
    }
}
