using System.Runtime.Serialization;

namespace DigitalWareFacturacion.JsonEtities
{
    [DataContract]
    public class DetalleFacturaJson
    {
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public string Precio { get; set; }
        [DataMember]
        public string IdProducto { get; set; }
    }
}
