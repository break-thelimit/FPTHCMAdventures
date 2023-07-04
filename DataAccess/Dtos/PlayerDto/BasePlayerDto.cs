using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public abstract class BasePlayerDto
    {
        public Guid? UserId { get; set; }
        public int? TotalPoint { get; set; }
        public int? TotalTime { get; set; }
    }
}
