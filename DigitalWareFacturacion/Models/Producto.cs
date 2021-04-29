using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public long PkIdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public long UltimoModificador { get; set; }
        public long FkIdCategoria { get; set; }

        public virtual Categoria FkIdCategoriaNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
