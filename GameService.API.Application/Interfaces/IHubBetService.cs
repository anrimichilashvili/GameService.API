using GameService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameService.API.Application.Interfaces
{
    public interface IHubBetService
    {
        Task<GameResultModel> RecordBetAsync(GameRound gameRound, string token);
    }
}
