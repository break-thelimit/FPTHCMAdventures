using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerHistoryDto
{
    public abstract class BasePlayerHistoryDto
    {
        public Guid EventtaskId { get; set; }
        public Guid PlayerId { get; set; }
        public double CompletedTime { get; set; }
        public double TaskPoint { get; set; }
        public string Status { get; set; }
    }
}
