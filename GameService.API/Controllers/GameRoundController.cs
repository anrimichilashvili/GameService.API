using GameService.API.Application.Interfaces;
using GameService.API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;

namespace GameService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameRoundController : ControllerBase
    {
        private readonly IGameSimulator _simulator;
        private readonly IHubBetService _hubBetService;
        public GameRoundController(IGameSimulator simulator, IHubBetService hubBetService, HttpClient httpClient    )
        {
            _simulator = simulator;
            _hubBetService = hubBetService;
        }

        [HttpPost]
        public async Task<ActionResult> PlayRoundAsync(decimal betAmount,string token)
        {
            GameRound round = _simulator.PlayRound(betAmount);

            var result =  await _hubBetService.RecordBetAsync(round,token);
            if(result==HttpStatusCode.OK)
                return Ok(result);
            else return BadRequest(result);
        }
    }
}
