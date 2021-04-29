using System.Runtime.Serialization;

namespace DigitalWareFacturacion.JsonEtities
{
    [DataContract]
    public class CategoriaJson
    {
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
