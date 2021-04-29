using System;
using DigitalWareFacturacion.Data;
using DigitalWareFacturacion.JsonEtities;
using DigitalWareFacturacion.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWareFacturacion.Controllers
{
    using Context_t = DIGITALWARE_DBContext;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private const string SUCCESS = "{\"success\":true, \"message\": \"Datos insertados correctamente\"}";
        private const string ERROR = "{\"success\":false, \"error\": \"Ha ocurrido un error al insertar el registro\"}";

        private readonly ProductoDao productoDao = new ProductoDao();

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                JsonResult Respuesta = productoDao.GetAllProducts();
                return Respuesta ?? Json(ERROR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Post([FromBody] ProductoJson productJson)
        {
            try
            {
                bool Respuesta = productoDao.ProcessProductJson(productJson);
                return (Respuesta ? SUCCESS : ERROR);
            }
            catch (Exception ex)
            {
                string Message = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    Message = Message + " Internal Exception: " + ex.InnerException.ToString();
                }
                //Log.Error("{Message}", Message);
                return ERROR;
            }
        }

        [HttpPut("{id}")]
        public string Put(long id, [FromBody] ProductoJson productJson)
        {
            try
            {
                bool Respuesta = productoDao.ProcessProductJosnUpdate(productJson, id);
                return (Respuesta ? SUCCESS.Replace("insertados", "actualizados") : ERROR);
            }
            catch (Exception ex)
            {
                string Message = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    Message = Message + " Internal Exception: " + ex.InnerException.ToString();
                }
                //Log.Error("{Message}", Message);
                return ERROR;
            }
        }


        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            try
            {
                bool Respuesta = productoDao.DeleteProductById(id);
                return (Respuesta ? SUCCESS.Replace("insertados", "eliminados") : ERROR);
            }
            catch (Exception ex)
            {
                string Message = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    Message = Message + " Internal Exception: " + ex.InnerException.ToString();
                }
                //Log.Error("{Message}", Message);
                return ERROR;
            }
        }
    }
}
