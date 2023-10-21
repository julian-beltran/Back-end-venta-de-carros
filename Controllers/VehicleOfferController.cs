using Microsoft.AspNetCore.Mvc;
using CompraVentaCarrosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompraVentaCarrosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleOfferController : ControllerBase
    {
        // GET: api/<VehicleOfferController>
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    var listUSer = db.Vehiculos.Where(d => d.Aprovacion == true).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = listUSer;

                }

            }
            catch
            {

            }
            return Ok(oRespuesta);
        }

        // GET api/<VehicleOfferController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VehicleOfferController>
        [Authorize]
        [HttpPost]
        public IActionResult Add(VehicleRequest vehicleRequest )
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.IdPersona = vehicleRequest.IdPersona;
                    vehiculo.Carroseria = vehicleRequest.Carroseria;
                    vehiculo.Kilometraje = vehicleRequest.Kilometraje;
                    vehiculo.Linea = vehicleRequest.Linea;
                    vehiculo.Marca = vehicleRequest.Marca;
                    vehiculo.Modelo = vehicleRequest.Modelo;
                    vehiculo.Placa = vehicleRequest.Placa;
                    vehiculo.Precio = vehicleRequest.Precio;
                    vehiculo.Imagen1 = vehicleRequest.Imagen1;
                    vehiculo.Imagen2 = vehicleRequest.Imagen2;
                    vehiculo.Imagen3 = vehicleRequest.Imagen3;
                    vehiculo.Aprovacion = false;
                    db.Vehiculos.Add(vehiculo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Oferta enviada al administrador";
                }

            }
            catch
            {
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "llene de manera correcta el formulario";

            }

            return Ok(oRespuesta);
        }

        // PUT api/<VehicleOfferController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehicleOfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
