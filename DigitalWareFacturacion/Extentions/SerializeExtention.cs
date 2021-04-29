using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using Serilog;


namespace DigitalWareFacturacion.Extentions
{
    public static class SerializeExtention
    {
        #region Deserialize
        /// <summary>
        /// Deserializacion del objeto Json recibido por el motodo POST
        /// </summary>
        /// <param name="JsonListMetaData">String a deserializar</param>
        /// <returns></returns>
        internal static T DeserializeData<T>(this string JsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonData);
            }
            catch (Exception ex)
            {
                var Exception = "Error message: " + ex.Message;
                if (ex.InnerException != null)
                {
                    Exception = Exception + " Inner exception " + ex.InnerException.Message;
                }
                //Log.Error("{Message}", Exception);
                return (T) new Object();
            }
        }

        #endregion Deserialize
    }
}
