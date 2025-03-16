using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.API.Domain.Models
{
    public class GameRound
    {
        public decimal BetAmount { get; set; }
        public bool IsWin { get; set; }
    }
}
