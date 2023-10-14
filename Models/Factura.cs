using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Factura
    {
        public int Id { get; set; }
        public long? IdVenta { get; set; }
        public decimal? Precio { get; set; }
        public decimal? PagoVehiculo { get; set; }
        public decimal? Importe { get; set; }
        public int? IdProducto { get; set; }

        public virtual Vehiculo IdProductoNavigation { get; set; }
        public virtual Ventum IdVentaNavigation { get; set; }
    }
}
