using GameService.API.Application.Interfaces;
using GameService.API.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameService.API.Infrastructure.Services
{
    public class HubBetService : IHubBetService
    {
        private readonly HttpClient _httpClient;
        private readonly string _betEndpoint;

        public HubBetService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _betEndpoint = configuration["HubService:BetEndpoint"];
        }

        public async Task<GameResultModel> RecordBetAsync(GameRound gameRound, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var betDto = new
            {
                Amount = gameRound.BetAmount,
                IsWin = gameRound.IsWin
            };
            var result = await _httpClient.PostAsJsonAsync(_betEndpoint, betDto);
            var gameResult = await result.Content.ReadFromJsonAsync<GameResultModel>();
            return gameResult;

        }
    }
}
