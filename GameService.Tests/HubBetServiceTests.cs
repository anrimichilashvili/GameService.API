using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GameService.API.Domain.Models;
using GameService.API.Infrastructure.Services;
using GameService.API.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Xunit;

namespace GameService.Tests
{
    public class HubBetServiceTests
    {

        private readonly Mock<IHubBetService> _hubBetServiceMock;

        public HubBetServiceTests()
        {
            _hubBetServiceMock = new Mock<IHubBetService>();
        }


        [Fact]
        public async Task RecordBetAsync_SendsCorrectPostRequest()
        {
            var gameRound = new GameRound
            {
                BetAmount = 50,
                IsWin = true
            };

            string dummyToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFucmkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTc0MjE0NzU0NSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxMiIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTIifQ.qYfK-q2ju4Dy9HjirhL8o2hcIvUPKUMW_DnVxrws2cWaBzinXmCx_8UycQbVYvgwwPDD3IP5Vq1pBphnMkg1GQ";

            _hubBetServiceMock
                .Setup(service => service.RecordBetAsync(gameRound, dummyToken))
                .ReturnsAsync(HttpStatusCode.OK);

            var result = await _hubBetServiceMock.Object.RecordBetAsync(gameRound, dummyToken);

            Assert.Equal(HttpStatusCode.OK, result);
            _hubBetServiceMock.Verify(service => service.RecordBetAsync(gameRound, dummyToken), Times.Once);


        }
    }
}
