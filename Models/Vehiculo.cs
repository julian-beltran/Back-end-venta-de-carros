using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Facturas = new HashSet<Factura>();
            Mensajes = new HashSet<Mensaje>();
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public int? Modelo { get; set; }
        public string Carroseria { get; set; }
        public long? Kilometraje { get; set; }
        public string Placa { get; set; }
        public decimal? Precio { get; set; }
        public int? IdPersona { get; set; }
        public bool? Aprovacion { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? EstadoCompra { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
