using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace CoreQRS
{
    public class CoreQRSMiddleware
    {
        private readonly RequestDelegate _next;
        public CoreQRSMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {

            await _next.Invoke(httpContext);
        }
    }
}