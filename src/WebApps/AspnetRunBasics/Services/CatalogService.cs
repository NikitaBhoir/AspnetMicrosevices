﻿
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService: ICatalogService
    {
   
        private readonly HttpClient _client;    //used to consume external apis, in theis sevice we need to consume api so for that we use httpclient

        public CatalogService(HttpClient client, ILogger<CatalogService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/Catalog");  //send httpget request to given base url which we configure in appsettings i.e to ocelot gateway
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/Catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var response = await _client.PostAsJson($"/Catalog", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CatalogModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
