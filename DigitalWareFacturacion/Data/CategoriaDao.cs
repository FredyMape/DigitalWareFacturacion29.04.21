using DigitalWareFacturacion.JsonEtities;
using DigitalWareFacturacion.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalWareFacturacion.Data
{
    using Context_t = DIGITALWARE_DBContext;

    public class CategoriaDao : Controller
    {
        #region Variables
        Categoria Categoria { get; set; }
        #endregion

        DIGITALWARE_DBContext Context = new DIGITALWARE_DBContext();

        #region Public Methods

        public JsonResult GetAllCategories() => GetCategories();

        public bool ProcessCategoryJson(CategoriaJson categoryJson) => 
            categoryJson?.GetType() == typeof(CategoriaJson) && SaveCategory(categoryJson);

        #endregion

        #region Querys DataBase

        private JsonResult GetCategories()
        {
            var result = (from A in Context.Categoria
                          orderby A.Nombre
                          select new
                          {
                              Name = A.Nombre
                          }).Distinct();
            return Json(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Consulta una categoria espesifica por nombre de la aplicacion
        /// </summary>
        /// <param name="categoryName">Nombre de la categoria por el cual se va a consultar</param>
        /// <returns></returns>
        private Categoria GetCategoryByName(string categoryName)
        {
            return Context.Categoria
                    .Where(x => x.Nombre == categoryName)
                    .First();
        }

        /// <summary>
        /// Valida la existencia de una aplicacion en la base de datos
        /// </summary>
        /// <param name="applicationName">Nombre de la aplicacion que se validara</param>
        /// <returns></returns>
        private bool ValidateCategoryByName(string categoryName)
        {
            return Context.Categoria
                    .Any(x => x.Nombre == categoryName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registra nueva categoria en la base de datos
        /// </summary>
        /// <param name="categoryName">Nombre de la categoria que valida su existencia</param>
        private bool SaveCategory(CategoriaJson category)
        {
            bool result = ValidateCategoryByName(category.CategoryName);
            if (!result)
            {
                Categoria = new Categoria()
                {
                    Nombre = category.CategoryName,
                    Descripcion = category.Description,
                    FechaRegistro = DateTime.Now,
                    UltimoModificador = 1                   

                };

                Context.Categoria.Add(Categoria);
                Context.SaveChanges();

                //_CategoriaDao.Update<Categoria>(Categoria, x => x.Nombre == category.CategoryName);
            }
            return !result;
        }

        #endregion

    }
}
