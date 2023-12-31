﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WDA.ApiDotNet.Business.Helpers
{
    public static class Extensions
    {
        public static void AddPagination<T>(this HttpResponse response,
            int pageNumber, int itemsPerpage, int totalitems, int totalPage)
        {
            var paginationHeader = new PaginationHeader<T>(pageNumber, itemsPerpage, totalitems, totalPage);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}