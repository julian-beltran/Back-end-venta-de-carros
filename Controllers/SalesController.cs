using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompraVentaCarrosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
       //Agregar Venta
        [HttpPost]
        public IActionResult AddSale(SalesRequest salesRequest)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Ventum venta = new Ventum();

                    venta.Fecha = DateTime.Now;

                    venta.IdVehiculo = salesRequest.IdVehiculo;
                    venta.IdPago = salesRequest.IdPago;
                    var usuario = db.Usuarios.Find(salesRequest.IdCliente);
                    var idPersona = db.Personas.Find(usuario.IdPersona);
                    venta.IdCliente = idPersona.Id;
                    venta.NombreTitular = salesRequest.NombreTitular;
                    venta.ApellidoTitular = salesRequest.ApellidoTitular;
                    venta.CorreoPaypal = salesRequest.CorreoPaypal;
                    db.Venta.Add(venta);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Compra Registrada";

                }
            }
            catch
            {
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "error del sistema";
            }
            return Ok(oRespuesta);
        }


        //obtener compras por usuario
        [HttpGet("MySales/{id}")]
        public IActionResult GetShops(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<SalesUserRequest> lst = new List<SalesUserRequest>();
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Usuario OUsuario = db.Usuarios.Find(id);
                    Persona Opersona = db.Personas.Find(OUsuario.IdPersona);



                    lst = (from a in db.Venta
                           where a.IdVehiculoNavigation.IdPersona == Opersona.Id
                           select new SalesUserRequest
                           {
                               Id = a.Id,
                               Fecha = a.Fecha,
                               NombreTitular = a.NombreTitular,
                               ApellidoTitular = a.ApellidoTitular,
                               CorreoPaypal = a.CorreoPaypal,
                               Nombre = a.IdClienteNavigation.Nombre,
                               Apellido = a.IdClienteNavigation.Apellido,
                               Telefono = a.IdClienteNavigation.Telefono,
                               IdVehicle = (int)a.IdVehiculo,
                               Marca = a.IdVehiculoNavigation.Marca,
                               Linea = a.IdVehiculoNavigation.Linea,
                               Modelo = a.IdVehiculoNavigation.Modelo,
                               Carroseria = a.IdVehiculoNavigation.Carroseria,
                               Kilometraje = a.IdVehiculoNavigation.Kilometraje,
                               Placa = a.IdVehiculoNavigation.Placa,
                               Precio = a.IdVehiculoNavigation.Precio,
                               IdPersona = a.IdCliente,
                               Imagen1 = a.IdVehiculoNavigation.Imagen1,
                               Imagen2 = a.IdVehiculoNavigation.Imagen2,
                               Imagen3 = a.IdVehiculoNavigation.Imagen3,
                               TipoVenta = a.IdPagoNavigation.TipoVenta,
                               Imagen = a.IdClienteNavigation.Imagen



                           }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch
            {
                oRespuesta.Mensaje = "Erros del sistema";
            }

            return Ok(oRespuesta);
        }

    }
}
