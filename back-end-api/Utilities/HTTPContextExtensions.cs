using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Utilities
{
    public static class HTTPContextExtensions
    {
        public async static Task InsertParamsPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable){

            if(httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalCountRecords", count.ToString());
        }
    }
}
