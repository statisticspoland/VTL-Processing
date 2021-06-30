namespace Service.Tests.ControllersTests
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using StatisticsPoland.VtlProcessing.Service.Controllers;
    using StatisticsPoland.VtlProcessing.Service.Models;
    using StatisticsPoland.VtlProcessing.Service.Services;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class TranslatorControllerTests
    {
        private TranslatorController _translatorController;
        private Mock<ITranslationService> _translationServiceMock;

        public TranslatorControllerTests()
        {
            var loggerMock = new Mock<ILogger<TranslatorController>>();
            _translationServiceMock = new Mock<ITranslationService>();
            _translatorController = new TranslatorController(loggerMock.Object, _translationServiceMock.Object);
        }        

        [Fact]
        public async Task TranslatorController_return_stream()
        {
            _translationServiceMock.Setup<TranslationResponse>(m => m.Tanslate(It.IsAny<TranslationParameters>())).Returns(new TranslationResponse("succes"));

            IActionResult result = await _translatorController.Get(new TranslationParameters(), "text/plain");

            Assert.True(result.GetType() == typeof(FileStreamResult));
        }

        [Fact]
        public async Task TranslatorController_return_json()
        {
            _translationServiceMock.Setup<TranslationResponse>(m => m.Tanslate(It.IsAny<TranslationParameters>())).Returns(new TranslationResponse("succes"));

            IActionResult result = await _translatorController.Get(new TranslationParameters(), "application/json");

            HttpStatusCode statusCode = (HttpStatusCode)result.GetType().GetProperty("StatusCode").GetValue(result, null);

            Assert.True(result.GetType() == typeof(OkObjectResult));
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task TranslatorController_default_return()
        {
            _translationServiceMock.Setup<TranslationResponse>(m => m.Tanslate(It.IsAny<TranslationParameters>())).Returns(new TranslationResponse("succes"));

            IActionResult result = await _translatorController.Get(new TranslationParameters(), "");

            HttpStatusCode statusCode = (HttpStatusCode)result.GetType().GetProperty("StatusCode").GetValue(result, null);

            Assert.True(result.GetType() == typeof(OkObjectResult));
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task TranslatorController_BadRequest()
        {
            _translationServiceMock.Setup<TranslationResponse>(m => m.Tanslate(It.IsAny<TranslationParameters>())).Returns(new TranslationResponse(new ArgumentException("test")));

            IActionResult result = await _translatorController.Get(new TranslationParameters(), "");

            HttpStatusCode statusCode = (HttpStatusCode)result.GetType().GetProperty("StatusCode").GetValue(result, null);

            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
    }
}
