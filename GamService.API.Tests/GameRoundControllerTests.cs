using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using GameService.API.Controllers;
using GameService.API.Application.Interfaces;
using GameService.API.Domain.Models;

namespace GameService.Tests
{
    public class GameRoundControllerTests
    {
        [Fact]
        public async Task PlayRoundAsync_ReturnsOk_WhenHubBetServiceReturnsResult()
        {
            decimal testBetAmount = 50;
            var expectedGameRound = new GameRound { BetAmount = testBetAmount, IsWin = true };
            var expectedGameResult = new GameResultModel { IsWin = true, Amount = testBetAmount };

            var mockSimulator = new Mock<IGameSimulator>();
            mockSimulator.Setup(sim => sim.PlayRound(testBetAmount))
                         .Returns(expectedGameRound);

            var mockHubBetService = new Mock<IHubBetService>();
            mockHubBetService.Setup(hbs => hbs.RecordBetAsync(expectedGameRound, It.IsAny<string>()))
                             .ReturnsAsync(expectedGameResult);

            var dummyHttpClient = new HttpClient();
            var controller = new GameRoundController(mockSimulator.Object, mockHubBetService.Object, dummyHttpClient);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "test token";
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext };

            var actionResult = await controller.PlayRoundAsync(testBetAmount);

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(expectedGameResult, okResult.Value);
        }

        [Fact]
        public async Task PlayRoundAsync_ReturnsBadRequest_WhenHubBetServiceReturnsNull()
        {
            decimal testBetAmount = 50;
            var expectedGameRound = new GameRound { BetAmount = testBetAmount, IsWin = false };

            var mockSimulator = new Mock<IGameSimulator>();
            mockSimulator.Setup(sim => sim.PlayRound(testBetAmount))
                         .Returns(expectedGameRound);

            var mockHubBetService = new Mock<IHubBetService>();
            mockHubBetService.Setup(hbs => hbs.RecordBetAsync(expectedGameRound, It.IsAny<string>()))
                             .ReturnsAsync((GameResultModel)null);

            var dummyHttpClient = new HttpClient();
            var controller = new GameRoundController(mockSimulator.Object, mockHubBetService.Object, dummyHttpClient);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "test token";
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext };

            var actionResult = await controller.PlayRoundAsync(testBetAmount);

            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
