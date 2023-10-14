using CompraVentaCarrosApi.Models.Request;
using CompraVentaCarrosApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Services
{
    public interface IUserServices
    {
        UserResponse Auth(AuthRequest model);
    }
}
