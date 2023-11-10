using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Mensajes = new HashSet<Mensaje>();
            Pqrs = new HashSet<Pqr>();
            Usuarios = new HashSet<Usuario>();
            Vehiculos = new HashSet<Vehiculo>();
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Ocupacion { get; set; }
        public int? Sexo { get; set; }
        public string Direccion { get; set; }
        public int? Edad { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<Pqr> Pqrs { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
