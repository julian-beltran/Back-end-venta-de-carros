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
    [Route("[controller]")]
    [ApiController]
    public class PqrsController : Controller
    {
     
        [HttpPost]
        public IActionResult add(PqrsRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    Pqr pqrs = new Pqr();
                    var usuario = db.Usuarios.Find(oModel.IdCliente);
                    var idPersona = db.Personas.Find(usuario.IdPersona);
                    pqrs.IdCliente = idPersona.Id;
                    if(oModel.TipoPqrs == "peticion") { pqrs.TipoPqrs = 1; };
                    if (oModel.TipoPqrs == "queja") { pqrs.TipoPqrs = 2; };
                    if (oModel.TipoPqrs == "reclamo") { pqrs.TipoPqrs = 3; };
                    if (oModel.TipoPqrs == "sugerencia") { pqrs.TipoPqrs = 4; };
                    pqrs.Mensaje = oModel.Mensaje;

                    db.Pqrs.Add(pqrs);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "PQRS enviado correctamente al administrador";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);

        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            List<PqrsGetRequest> lst = new List<PqrsGetRequest>();
            try
            {

                using (VENTACARROSContext db = new VENTACARROSContext())
                {
                    lst=(from d in db.Pqrs
                        select new PqrsGetRequest
                        {
                            IdCliente = d.IdCliente,
                            TipoPqrs =d.TipoPqrsNavigation.Tipo,
                            Id = d.Id,
                            Mensaje=d.Mensaje,
                            Nombre=d.IdClienteNavigation.Nombre,
                            Apellido=d.IdClienteNavigation.Apellido,
                            Telefono=d.IdClienteNavigation.Telefono,
                            Imagen = d.IdClienteNavigation.Imagen


                        }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;

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
