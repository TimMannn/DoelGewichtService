using System.Security.Claims;
using DoelGewicht.Api.Data;
using DoelGewicht.Api.Models;
using DoelGewicht.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DoelGewicht.Tests.UnitTesten
{
    [TestClass]
    public class DoelGewichtTests
    {
        private Mock<IDoelGewichtData> _gewichtDataMock = null!;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock = null!;
        private DoelGewichtService _gewichtService = null!;

        private const string TestUserId = "test-user-id";

        [TestInitialize]
        public void Setup()
        {
            _gewichtDataMock = new Mock<IDoelGewichtData>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, TestUserId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(identity)
            };

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _gewichtService = new DoelGewichtService(
                _gewichtDataMock.Object,
                _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public async Task GetDoelGewicht_CallsDataLayerWithUserIdAndReturnsList()
        {
            var expectedList = new List<DoelGewichtDetails>
            {
                new DoelGewichtDetails { Doelgewicht = 70.0, Id = 1 },
                new DoelGewichtDetails { Doelgewicht = 68.0, Id = 2 }
            };

            _gewichtDataMock.Setup(g => g.GetDoelGewicht(TestUserId)).ReturnsAsync(expectedList);

            var result = await _gewichtService.GetDoelGewicht();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(70.0, result[0].Doelgewicht);
            Assert.AreEqual(68.0, result[1].Doelgewicht);
            _gewichtDataMock.Verify(g => g.GetDoelGewicht(TestUserId), Times.Once);
        }

        [TestMethod]
        public async Task SetDoelGewicht_CallsDataLayerWithCorrectValueAndUserId()
        {
            double doelGewicht = 68.5;

            await _gewichtService.SetDoelGewicht(doelGewicht);

            _gewichtDataMock.Verify(g => g.SetDoelGewicht(doelGewicht, TestUserId), Times.Once);
        }

        [TestMethod]
        public async Task EditDoelGewicht_CallsDataLayerWithCorrectParametersAndUserId()
        {
            int id = 1;
            double? doelGewicht = 67.0;
            DateTime? datumBehaald = DateTime.Today;

            await _gewichtService.EditDoelGewicht(id, doelGewicht, datumBehaald);

            _gewichtDataMock.Verify(g => g.EditDoelGewicht(id, doelGewicht, datumBehaald, TestUserId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteDoelGewicht_CallsDataLayerWithCorrectIdAndUserId()
        {
            int id = 3;

            await _gewichtService.DeleteDoelGewicht(id);

            _gewichtDataMock.Verify(g => g.DeleteDoelGewicht(id, TestUserId), Times.Once);
        }
    }
}