using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Controllers
{
    public class ShopController : Controller
    {
        //obtener compras por usuario
        [HttpGet("MyShops/{id}")]
        public IActionResult GetShops(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<shopRequest> lst = new List<shopRequest>();
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Usuario OUsuario = db.Usuarios.Find(id);
                    Persona Opersona = db.Personas.Find(OUsuario.IdPersona);
                    lst=(from a in db.Venta
                        where a.IdCliente == Opersona.Id
                        select new shopRequest
                        {
                            Id = a.Id,
                            Fecha = a.Fecha,
                            NombreTitular = a.NombreTitular,
                            ApellidoTitular =a.ApellidoTitular,
                            CorreoPaypal = a.CorreoPaypal,
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
                            Imagen2=a.IdVehiculoNavigation.Imagen2,
                            Imagen3=a.IdVehiculoNavigation.Imagen3,
                            TipoVenta = a.IdPagoNavigation.TipoVenta



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
