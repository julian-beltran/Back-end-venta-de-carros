using CompraVentaCarrosApi.Models;
using CompraVentaCarrosApi.Models.Commons;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using CompraVentaCarrosApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Controllers
{
    public class VehicleUserController : Controller
    {
        public IAzureStorage _azureStorage;

        public VehicleUserController(IAzureStorage azureStorage)
        {
            _azureStorage = azureStorage;

        }

        // GET api/<VehicleOfferController>/5
        [HttpGet("getOffers/{id}")]
        public IActionResult GetOffers(int id)
        {

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<VehicleRequestUser> getOffers = null;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var user = db.Usuarios.Find(id);
                    var persona = db.Personas.Find(user.IdPersona);

                    getOffers = (from d in db.Vehiculos
                                 where d.IdPersona == persona.Id
                                 select new VehicleRequestUser
                                 {
                                     Id=d.Id,
                                     Marca=d.Marca,
                                     Linea=d.Linea,
                                     Modelo=d.Modelo,
                                     Carroseria=d.Carroseria,
                                     Kilometraje=d.Kilometraje,
                                     Placa=d.Placa,
                                     Precio=d.Precio,
                                     Imagen1=d.Imagen1,
                                     Imagen2=d.Imagen2,
                                     Imagen3=d.Imagen3,
                                     Descripcion=d.Descripcion,
                                     Fecha=d.Fecha,
                                     EstadoCompra=d.EstadoCompra,
                                     Aprovacion = d.Aprovacion
                                                     

                                 }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Mis Ofertas";
                    oRespuesta.Data = getOffers;
                }

            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "ocurrio un error";
            }
            return Ok(oRespuesta);

        }


        [HttpPut("editarOferta/{id}")]
        public IActionResult EditOffer(int id,[FromForm] VehicleRequest vehicleRequest)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                   
                    Vehiculo vehiculo = db.Vehiculos.Find(id);
                    vehiculo.IdPersona = vehicleRequest.IdPersona;
                    vehiculo.Carroseria = vehicleRequest.Carroseria;
                    vehiculo.Kilometraje = vehicleRequest.Kilometraje;
                    vehiculo.Linea = vehicleRequest.Linea;
                    vehiculo.Marca = vehicleRequest.Marca;
                    vehiculo.Modelo = vehicleRequest.Modelo;
                    vehiculo.Placa = vehicleRequest.Placa;
                    vehiculo.Precio = vehicleRequest.Precio;
                    vehiculo.Fecha = DateTime.Now;
                    if (vehicleRequest.Imagen1 is not null)
                    {
                        vehiculo.Imagen1 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen1);
                    }
                    if (vehicleRequest.Imagen2 is not null)
                    {
                        vehiculo.Imagen2 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen2);
                    }
                    if (vehicleRequest.Imagen3 is not null)
                    {
                        vehiculo.Imagen3 = _azureStorage.SaveFile(AzureContainers.USERS, vehicleRequest.Imagen3);
                    }
                    if (vehiculo.Imagen1 == null || vehiculo.Imagen2 == null || vehicleRequest.Imagen3 == null)
                    {
                        oRespuesta.Exito = 0;
                        oRespuesta.Mensaje = "Es necesario al menos tres imagenes del vehiculo";
                    }
                    else
                    {
                        vehiculo.Descripcion = vehicleRequest.Descripcion;
                        vehiculo.Aprovacion = false;
                        var usuario = db.Usuarios.Find(vehicleRequest.IdPersona);
                        var idPersona = db.Personas.Find(usuario.IdPersona);
                        vehiculo.IdPersona = idPersona.Id;
                        db.Entry(vehiculo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        oRespuesta.Exito = 1;
                        oRespuesta.Mensaje = "Oferta re-enviada al administrador";
                    }
                }

            }
            catch
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "llene de manera correcta el formulario";

            }

            return Ok(oRespuesta);
        }


        [HttpDelete("eliminarOferta/{id}")]
        public IActionResult DeleteOffer(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Vehiculo vehiculo = db.Vehiculos.Find(id);
                    db.Remove(vehiculo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "la oferta fue eliminada de manera permanente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }
    }
}
