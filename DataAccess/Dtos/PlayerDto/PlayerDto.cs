using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public class PlayerDto
    {
        public string StudentName { get; set; }
        public string EventName { get; set; }
        public string Nickname { get; set; }
        public string Passcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public double TotalPoint { get; set; }
        public double TotalTime { get; set; }
        public bool Isplayer { get; set; }

    }
}