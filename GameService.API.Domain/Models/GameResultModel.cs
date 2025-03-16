﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.API.Domain.Models
{
    public class GameResultModel
    {
        public decimal Amount { get; set; }
        public DateTime PlacedAt { get; set; }
        public bool IsWin { get; set; } = false;
    }
}
