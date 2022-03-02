using System.Runtime.Serialization.Json;
using System.Net;
using API.helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(
            this HttpResponse response,
            int correntPage,
            int itemPerPage,
            int totalItems,
            int totalPages
        )
        {
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var paginationHeaders = new PaginationHeaders(correntPage, itemPerPage,totalItems,totalPages );
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeaders, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}   
