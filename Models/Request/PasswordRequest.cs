using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Models.Request
{
    public class PasswordRequest
    {
        public string email { get; set; }
        public string emailToken { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
