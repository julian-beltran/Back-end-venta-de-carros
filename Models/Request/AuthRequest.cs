﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string Usuario1 { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
