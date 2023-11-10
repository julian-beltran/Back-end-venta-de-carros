using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Pqr
    {
        public int Id { get; set; }
        public int? TipoPqrs { get; set; }
        public int? IdCliente { get; set; }
        public string Mensaje { get; set; }

        public virtual Persona IdClienteNavigation { get; set; }
        public virtual TipoPqr TipoPqrsNavigation { get; set; }
    }
}
