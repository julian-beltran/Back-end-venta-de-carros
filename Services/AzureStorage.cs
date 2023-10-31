using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Services
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string _connectionString;
        public AzureStorage(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }
        

        public string SaveFile(string container, IFormFile file)
        {
            var client = new BlobContainerClient(_connectionString,container);
             client.CreateIfNotExists();
             client.SetAccessPolicy(PublicAccessType.Blob);
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);
            blob.Upload(file.OpenReadStream());
            return blob.Uri.ToString();
        }
        public string EditFile(string container, IFormFile file, string route)
        {
            DeleteFile(container, route);
            return SaveFile(container, file);
        }
        public void DeleteFile(string container, string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                return;
            }
            var client = new BlobContainerClient(_connectionString, container);
             client.CreateIfNotExists();
            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            blob.DeleteIfExists();
        }
    }
}
