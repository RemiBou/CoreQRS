using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
namespace CoreQRS.Test
{
    public class CoreQRSMiddlewareTest
    {
        private readonly CoreQRSMiddleware sut;
        private readonly Mock<RequestDelegate> delegateMock;
        private readonly Mock<HttpContext> contextMock;
        private readonly Mock<ICoreQRSQueryFactory> queryFactoryMock;
        private readonly Mock<HttpRequest> requestMock;

        public CoreQRSMiddlewareTest()
        {
            delegateMock = new Mock<RequestDelegate>();
            contextMock = new Mock<HttpContext>();
            requestMock = new Mock<HttpRequest>();
            contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            queryFactoryMock = new Mock<ICoreQRSQueryFactory>();
            sut = new CoreQRSMiddleware(delegateMock.Object,queryFactoryMock.Object);
        }
        [Fact]
        public void Middleware_Calls_Next()
        {   
            sut.Invoke(contextMock.Object);
            delegateMock.Verify(f => f(contextMock.Object), Times.Once);
        }

        [Fact]
        public void Middleware_Calls_QueryFactory_IfGetRequest()
        {       
             
            //Given
            requestMock.SetupGet(r => r.Method).Returns("GET");    
            //When
            sut.Invoke(contextMock.Object).Wait();
            //Then
            queryFactoryMock.Verify(f => f.CreateQuery(contextMock.Object), Times.Once);
        }
     
    }
}