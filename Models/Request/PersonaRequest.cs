using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class PersonaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Ocupacion { get; set; }
        public int? Sexo { get; set; }
        public string Direccion { get; set; }
        public int? Edad { get; set; }
    }
}
