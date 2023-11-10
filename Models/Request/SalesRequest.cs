using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class SalesRequest
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdCliente { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdPago { get; set; }
        public string NombreTitular { get; set; }
        public string ApellidoTitular { get; set; }
        public string CorreoPaypal { get; set; }

    }
    public class SalesUserRequest
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string NombreTitular { get; set; }
        public string ApellidoTitular { get; set; }
        public string CorreoPaypal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int IdVehicle { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int? Modelo { get; set; }
        public string Carroseria { get; set; }
        public long? Kilometraje { get; set; }
        public string Placa { get; set; }
        public decimal? Precio { get; set; }
        public int? IdPersona { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string TipoVenta { get; set; }
        public string Imagen { get; set; }


    }
}
