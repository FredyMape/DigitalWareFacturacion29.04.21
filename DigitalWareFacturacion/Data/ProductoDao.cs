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

    public class ProductoDao : Controller
    {
        #region Variables
        Producto Producto { get; set; }

        #endregion

        DIGITALWARE_DBContext Context = new DIGITALWARE_DBContext();

        #region Public Methods

        public JsonResult GetAllProducts() => GetProducts();
        public bool DeleteProductById(long invoiceId) => DeleteProduct(invoiceId);
        public bool UpdateStockProductById(long productId, byte stock) => UpdateProductStock(productId, stock);
        public bool ProcessProductJson(ProductoJson productoJson) =>
            productoJson?.GetType() == typeof(ProductoJson) && SaveProduct(productoJson);
        public bool ProcessProductJosnUpdate(ProductoJson productoJson, long productId) =>
            productoJson?.GetType() == typeof(ProductoJson) && UpdateProduct(productoJson, productId);

        #endregion

        #region Querys DataBase

        /// <summary>
        /// Obtiene un listado de las facturas registradas en la base de datos
        /// </summary>
        /// <returns></returns>
        private JsonResult GetProducts()
        {
            var result = (from A in Context.Productos
                          join B in Context.Categoria on A.FkIdCategoria equals B.PkIdCategoria into B_join
                          from B in B_join
                          orderby A.Nombre
                          select new
                          {
                              CATEGORIA = B.Nombre,
                              DESCRIPCION_CATEGORIA = B.Descripcion,
                              PRODUCTO = A.Nombre,
                              PRECIO = A.Precio,
                              STOCK = A.Stock
                          }).Distinct();
            return Json(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Consulta una producto espesifico por id del producto
        /// </summary>
        /// <param name="productId">Id del producto por el cual se va a consultar</param>
        /// <returns></returns>
        private Producto GetProductById(long productId)
        {
            return Context.Productos
                    .Where(x => x.PkIdProducto == productId)
                    .First();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registra un nuevo producto en la base de datos
        /// </summary>
        /// <param name="producto">Objeto producto a insertar</param>
        private bool SaveProduct(ProductoJson producto)
        {
            try
            {
                Producto = new Producto()
                {
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    FechaRegistro = DateTime.Now,
                    UltimoModificador = 1,
                    FkIdCategoria = producto.IdCategoria
                };

                Context.Productos.Add(Producto);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Modifica un producto en la base de datos
        /// </summary>
        /// <param name="producto">Objeto producto a modificar</param>
        private bool UpdateProduct(ProductoJson producto, long productId)
        {
            try
            {
                Producto currentProduct = GetProductById(productId);
                if (currentProduct == null)
                {
                    return false;
                }

                currentProduct.Nombre = producto.Nombre;
                currentProduct.Precio = producto.Precio;
                currentProduct.Stock = producto.Stock;
                currentProduct.FechaActualizacion = DateTime.Now;
                currentProduct.FkIdCategoria = producto.IdCategoria;
                
                Context.Productos.Update(currentProduct);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Actualiza el stock de un producto en la base de datos
        /// </summary>
        /// <param name="productId">Id del producto que valida su existencia</param>
        private bool UpdateProductStock(long productId, byte stock)
        {
            try
            {
                Producto currentProduct = GetProductById(productId);
                if (currentProduct == null)
                {
                    return false;
                }
                byte.TryParse(currentProduct.Stock, out byte currentStock);

                currentProduct.Stock = (currentStock - stock).ToString();
                currentProduct.FechaActualizacion = DateTime.Now;
                Context.Productos.Update(currentProduct);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina un producto en la base de datos
        /// </summary>
        /// <param name="productId">Id del producto que valida su existencia</param>
        private bool DeleteProduct(long productId)
        {
            try
            {
                Producto currentProduct = GetProductById(productId);
                if (currentProduct == null)
                {
                    return false;
                }

                Context.Productos.Remove(currentProduct);
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

    }
}
