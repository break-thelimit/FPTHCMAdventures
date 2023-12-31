﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid EventId { get; set; }
        public string StudentName { get; set; }

        public string StudentEmail { get; set; }
        public string EventName { get; set; }
        public string Nickname { get; set; }
        public string Passcode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public double TotalPoint { get; set; }
        public double TotalTime { get; set; }
        public bool Isplayer { get; set; }

    }
}