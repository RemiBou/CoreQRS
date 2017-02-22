
using Microsoft.AspNetCore.Http;
public interface ICoreQRSQueryFactory{
    IQuery CreateQuery(HttpContext context);
}