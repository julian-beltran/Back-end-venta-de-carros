using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class TipoVentum
    {
        public TipoVentum()
        {
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string TipoVenta { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
