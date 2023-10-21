using CompraVentaCarrosApi.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Services
{
    public interface IEmailServices
    {
        void SendEmail(EmailModel emailModel);
    }
}
