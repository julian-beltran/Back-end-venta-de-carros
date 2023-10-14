using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class RegisterRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Ocupacion { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public int? Edad { get; set; }
        public string Usuario1 { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string rol { get; set; }
        public int IdPersona { get; set; }
    }
}
