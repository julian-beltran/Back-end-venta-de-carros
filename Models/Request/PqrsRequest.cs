using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class PqrsRequest
    {
        public int Id { get; set; }
        public string TipoPqrs { get; set; }
        public int? IdCliente { get; set; }
        public string Mensaje { get; set; }
    }

    public class PqrsGetRequest
    {
        public int Id { get; set; }
        public string TipoPqrs { get; set; }
        public int? IdCliente { get; set; }
        public string Mensaje { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Imagen { get; set; }
    }
}
