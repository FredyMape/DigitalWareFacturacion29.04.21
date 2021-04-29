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

    public class FacturaDao : Controller
    {
        #region Variables
        Factura Factura { get; set; }
        DetalleFactura DetalleFactura { get; set; }
        #endregion

        DIGITALWARE_DBContext Context = new DIGITALWARE_DBContext();

        #region Public Methods

        public JsonResult GetAllInvoices() => GetInvoices();
        public bool DeleteInvoiceById(long invoiceId) => DeleteInvoice(invoiceId);

        public bool ProcessInvoiceJson(FacturaJson facturaJson) =>
            facturaJson?.GetType() == typeof(FacturaJson) && SaveInvoice(facturaJson);

        #endregion

        #region Querys DataBase

        /// <summary>
        /// Obtiene un listado de las facturas registradas en la base de datos
        /// </summary>
        /// <returns></returns>
        private JsonResult GetInvoices()
        {
            var result = (from A in Context.Facturas
                          join B in Context.DetalleFacturas on A.PkIdFactura equals B.FkIdFactura into B_join
                          from B in B_join
                          join C in Context.Clientes on A.FkIdCliente equals C.PkIdCliente into C_join
                          from C in C_join
                          join D in Context.TipoPagos on A.FkIdTipoPago equals D.PkIdTipoPago into D_join
                          from D in D_join
                          join E in Context.Productos on B.FkIdProducto equals E.PkIdProducto into E_join
                          from E in E_join
                          orderby A.PkIdFactura
                          select new
                          {
                              ID_FACTURA = A.PkIdFactura,
                              FECHA =  A.FechaRegistro,
                              CLIENTE = C.Nombre + " " + C.Apellido,
                              TIPO_PAGO = D.Nombre,
                              DESCRIPCION = D.Descripcion,
                              PRODUCTO = E.Nombre,
                              PRECIO = B.Precio,
                              CANTIDAD = B.Cantidad

                          }).Distinct();
            return Json(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Consulta una factura espesifica por id de la factura
        /// </summary>
        /// <param name="facturaId">Id de la factura por la cual se va a consultar</param>
        /// <returns></returns>
        private Factura GetInvoiceById(long invoiceId)
        {
            return Context.Facturas
                    .Where(x => x.PkIdFactura == invoiceId)
                    .First();
        }

        /// <summary>
        /// Consulta una detalle de factura espesifica por id del detalle
        /// </summary>
        /// <param name="facturaId">Id de la factura por la cual se va a consultar</param>
        /// <returns></returns>
        private DetalleFactura GetInvoiceDetailById(long detailId)
        {
            return Context.DetalleFacturas
                    .Where(x => x.PkIdDetalleFactura == detailId)
                    .First();
        }

        /// <summary>
        /// Consulta todos los detalles de factura espesifica por id de la factura
        /// </summary>
        /// <param name="facturaId">Id de la factura por la cual se va a consultar</param>
        /// <returns></returns>
        private List<DetalleFactura> GetAllInvoiceDetailByInvoiceId(long invoiceId)
        {
            return Context.DetalleFacturas
                    .Where(x => x.FkIdFactura == invoiceId)
                    .ToList();
        }

        /// <summary>
        /// Consulta una factura por fecha de registro
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        private Factura GetInvoiceByParams(Factura invoice)
        {
            return Context.Facturas
                    .Where(x => x.FechaRegistro.Equals(invoice.FechaRegistro))
                    .First();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registra nueva factura en la base de datos
        /// </summary>
        /// <param name="factura">Objeto factura a insertar</param>
        private bool SaveInvoice(FacturaJson factura)
        {
            try
            {
                long.TryParse(factura.IdCliente, out long idCLiente);
                long.TryParse(factura.IdTipoPago, out long idTipoPago);

                Factura = new Factura()
                {
                    FechaRegistro = DateTime.Now,
                    UltimoModificador = 1,
                    FkIdCliente = idCLiente,
                    FkIdTipoPago = idTipoPago
                };

                Context.Facturas.Add(Factura);
                Context.SaveChanges();

                factura.DetalleFacturaList.ForEach(x => {
                    byte.TryParse(x.Cantidad, out byte idCantidad);
                    decimal.TryParse(x.Precio, out decimal Precio);
                    long.TryParse(x.IdProducto, out long idProducto);

                    DetalleFactura = new DetalleFactura()
                    {
                        Cantidad = idCantidad,
                        Precio = Precio,
                        FechaRegistro = DateTime.Now,
                        UltimoModificador = 1,
                        FkIdFactura = GetInvoiceByParams(Factura).PkIdFactura,
                        FkIdProducto = idProducto
                    };

                    Context.DetalleFacturas.Add(DetalleFactura);
                    Context.SaveChanges();

                    using ProductoDao productoDao = new ProductoDao();
                    productoDao.UpdateStockProductById(DetalleFactura.FkIdProducto, DetalleFactura.Cantidad);
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Elimina una factura en la base de datos
        /// </summary>
        /// <param name="categoryName">Nombre de la categoria que valida su existencia</param>
        private bool DeleteInvoice(long invoiceId)
        {
            try
            {
                Factura currentInvoice = GetInvoiceById(invoiceId);
                if (currentInvoice == null)
                {
                    return false;
                }

                GetAllInvoiceDetailByInvoiceId(invoiceId).ForEach(x => {
                    Context.DetalleFacturas.Remove(x);
                    Context.SaveChanges();
                });

                Context.Facturas.Remove(currentInvoice);
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
