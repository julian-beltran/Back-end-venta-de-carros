using System;
using System.Collections.Generic;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class TipoPqr
    {
        public TipoPqr()
        {
            Pqrs = new HashSet<Pqr>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Pqr> Pqrs { get; set; }
    }
}
