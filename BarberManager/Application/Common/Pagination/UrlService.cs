using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "http";
            // Use Host and Port separately. Host.Value can contain a port (eg. "localhost:5000")
            // which makes UriBuilder.Host invalid. Use Host.Host and Host.Port instead.
            string host = "localhost";
            int? port = null;
            if (request?.Host.HasValue == true)
            {
                host = request.Host.Host;
                port = request.Host.Port;
            }

            var query = $"pageNumber={filter.PageNumber}&pageSize={filter.PageSize}";

            var uriBuilder = new UriBuilder
            {
                Scheme = scheme,
                Host = host,
                Path = route,
                Query = query
            };

            if (port.HasValue)
            {
                uriBuilder.Port = port.Value;
            }

            return uriBuilder.Uri;
        }
    }
}
