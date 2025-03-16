using GameService.API.Application.Interfaces;
using GameService.API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameRoundController : ControllerBase
    {
        private readonly IGameSimulator _simulator;
        private readonly IHubBetService _hubBetService;
        public GameRoundController(IGameSimulator simulator, IHubBetService hubBetService, HttpClient httpClient)
        {
            _simulator = simulator;
            _hubBetService = hubBetService;
        }

        [HttpPost("Play")]
        public async Task<ActionResult> PlayRoundAsync([FromQuery] decimal betAmount)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            GameRound round = _simulator.PlayRound(betAmount);

            var result = await _hubBetService.RecordBetAsync(round, token);
            if (result!=null)
                return Ok(result);
            else return BadRequest();
        }
    }
}
