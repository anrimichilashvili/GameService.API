using GameService.API.Application.Interfaces;
using GameService.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.API.Application.Services
{
    public class SimpleGameSimulator : IGameSimulator
    {
        private readonly Random _random;

        public SimpleGameSimulator()
        {
            _random = new Random();
        }

        public GameRound PlayRound(decimal betAmount)
        {
            bool isWin = _random.NextDouble() > 0.5;

            return new GameRound
            {
                BetAmount = betAmount,
                IsWin = isWin,
            };
        }
    }
}
