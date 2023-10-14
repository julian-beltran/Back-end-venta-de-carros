using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Contraseña { get; set; }
        public int? IdPersona { get; set; }
        public string Email { get; set; }
        public int? IdRol { get; set; }


        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
