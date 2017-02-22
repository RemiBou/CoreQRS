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
        private readonly Mock<IQuery> queryMock;
        private readonly Mock<HttpResponse> responseMock;

        public CoreQRSMiddlewareTest()
        {
            delegateMock = new Mock<RequestDelegate>();
            contextMock = new Mock<HttpContext>();
            requestMock = new Mock<HttpRequest>();
            responseMock = new Mock<HttpResponse>();
            contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            contextMock.SetupGet(c => c.Response).Returns(responseMock.Object);
            queryFactoryMock = new Mock<ICoreQRSQueryFactory>();
            queryMock = new Mock<IQuery>();
            sut = new CoreQRSMiddleware(delegateMock.Object, queryFactoryMock.Object);
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
        [Fact]
        public void Middleware_Calls_Query_Execute_IfGetRequest()
        {
            //Given
            requestMock.SetupGet(r => r.Method).Returns("GET");
            queryFactoryMock.Setup(f => f.CreateQuery(contextMock.Object)).Returns(queryMock.Object);
            //When
            sut.Invoke(contextMock.Object).Wait();
            //Then
            queryMock.Verify(f => f.GetResultsAsync(), Times.Once);
        }
        [Fact]
        public void Middleware_SetResponse404_WhenGETAndFactoryReturnsNull()
        {
            //Given
            requestMock.SetupGet(r => r.Method).Returns("GET");
            queryFactoryMock.Setup(f => f.CreateQuery(contextMock.Object)).Returns((IQuery)null);
            //When
            sut.Invoke(contextMock.Object).Wait();
            //Then
            responseMock.VerifySet(r => r.StatusCode = 404);
        }

    }
}