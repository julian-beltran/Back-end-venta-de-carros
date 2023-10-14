using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            Facturas = new HashSet<Factura>();
        }

        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Total { get; set; }

        public virtual Persona IdClienteNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
