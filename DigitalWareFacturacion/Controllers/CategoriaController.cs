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
    public class CategoriaController : Controller
    {
        private const string SUCCESS = "{\"success\":true, \"message\": \"Datos insertados correctamente\"}";
        private const string ERROR = "{\"success\":false, \"error\": \"Ha ocurrido un error al insertar el registro\"}";

        private readonly CategoriaDao categoriaDao = new CategoriaDao();

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                JsonResult Respuesta = categoriaDao.GetAllCategories();
                return (Respuesta != null ? Respuesta : Json(ERROR));
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        [HttpPost]
        public string Post([FromBody] CategoriaJson categoriaJson)
        {
            try
            {
                bool Respuesta = categoriaDao.ProcessCategoryJson(categoriaJson);
                return (Respuesta == true ? SUCCESS : ERROR);
            }

            catch (Exception ex)
            {
                string Message = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    Message = Message + " Internal Exception: " + ex.InnerException.ToString();
                }
                return ERROR;
            }
        }
    }
}
