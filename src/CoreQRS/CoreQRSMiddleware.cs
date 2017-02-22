using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace CoreQRS
{
    public class CoreQRSMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICoreQRSQueryFactory _queryFactory;

        public CoreQRSMiddleware(RequestDelegate next,ICoreQRSQueryFactory queryFactory)
        {
            _next = next;
            _queryFactory = queryFactory;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Method =="GET"){
                var query = _queryFactory.CreateQuery(httpContext);
                if(query != null){
                    var res = await query.GetResultsAsync();
                }
                httpContext.Response.StatusCode = 404;
                //query result handler
            }
                      
            await _next.Invoke(httpContext);
        }
    }
}