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
        public int? IdVehiculo { get; set; }
        public int? IdPago { get; set; }
        public string NombreTitular { get; set; }
        public string ApellidoTitular { get; set; }
        public string CorreoPaypal { get; set; }

        public virtual Persona IdClienteNavigation { get; set; }
        public virtual TipoVentum IdPagoNavigation { get; set; }
        public virtual Vehiculo IdVehiculoNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
