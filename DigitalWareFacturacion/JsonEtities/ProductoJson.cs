using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DigitalWareFacturacion.JsonEtities
{
    [DataContract]
    public class ProductoJson
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public string Stock { get; set; }
        [DataMember]
        public long IdCategoria { get; set; }
    }
}
