using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
namespace CoreQRS.Test
{
    class CoreQRSMiddlewareTest
    {
        [Fact]
        public void Middleware_Calls_Next(){
            var delegateMock = new Mock<RequestDelegate>();
            var sut = new CoreQRSMiddleware(delegateMock.Object);
            var contextMock = new Mock<HttpContext>();
            sut.Invoke(contextMock.Object);
            delegateMock.Verify(f => f(contextMock.Object), Times.Once);
        }
    }
}