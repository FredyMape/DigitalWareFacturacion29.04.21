using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DigitalWareFacturacion.JsonEtities
{
    [DataContract]
    public class FacturaJson
    {
        [DataMember]
        public string IdCliente { get; set; }
        [DataMember]
        public string IdTipoPago { get; set; }
        [DataMember]
        public List<DetalleFacturaJson> DetalleFacturaList { get; set; }
    }
}
