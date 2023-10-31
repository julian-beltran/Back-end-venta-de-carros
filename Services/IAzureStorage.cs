using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Services
{
    public interface IAzureStorage
    {
        string SaveFile(string container, IFormFile file);
        string EditFile(string container, IFormFile file, string route);
        void DeleteFile(string container, string route);
    }
}
