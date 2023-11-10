using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class VehicleRequest
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int? Modelo { get; set; }
        public string Carroseria { get; set; }
        public long? Kilometraje { get; set; }
        public string Placa { get; set; }
        public decimal? Precio { get; set; }
        public IFormFile Imagen1 { get; set; }
        public IFormFile Imagen2 { get; set; }
        public IFormFile Imagen3 { get; set; }
        public int? IdPersona { get; set; }
        public bool? Aprovacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? EstadoCompra { get; set; }

    }

    public class Aprovacion
    {
        public bool? aprovacion { get; set; }
    }

    public class EstadoCompra
    {
        public bool? estadoCompra { get; set; }
    }



    public class GetOffers
    {
        public int IdVehicle { get; set; }
        public bool? Aprovacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Imagen { get; set; }
        public int IdPersona { get; set; }
        
    }

    public class GetOffer
    {
        public int IdVehicle { get; set; }
        public bool? Aprovacion { get; set; }
        public bool? EstadoCompra { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Imagen { get; set; }
        public int IdPersona { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int? Modelo { get; set; }
        public string Carroseria { get; set; }
        public long? Kilometraje { get; set; }
        public string Placa { get; set; }
        public decimal? Precio { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Descripcion { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public DateTime? Fecha { get; set; }
    }
    public class GetImagen
    {
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }

    }

    public class imagenResponse
    {
        public string imagen { get; set; }
      
    }

    public class MessageRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Pregunta { get; set; }
        public int? IdPersona { get; set; }
        public int? IdVehiculo { get; set; }

    }

    public class getMessage
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Pregunta { get; set; }
        public int? IdPersona { get; set; }
        public int? IdVehiculo { get; set; }
    }


    public class VehicleRequestUser
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int? Modelo { get; set; }
        public string Carroseria { get; set; }
        public long? Kilometraje { get; set; }
        public string Placa { get; set; }
        public decimal? Precio { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public int? IdPersona { get; set; }
        public bool? Aprovacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? EstadoCompra { get; set; }

    }
}
