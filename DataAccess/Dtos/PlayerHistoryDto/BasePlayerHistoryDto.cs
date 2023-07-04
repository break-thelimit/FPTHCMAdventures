using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerHistoryDto
{
    public abstract class BasePlayerHistoryDto
    {
        public Guid? TaskId { get; set; }
        public Guid? PlayerId { get; set; }
        public DateTime? AcceptTime { get; set; }
        public TimeSpan? CompleteTime { get; set; }
        public int? Time { get; set; }
        public int? TaskPoint { get; set; }
        public string Status { get; set; }

    }
}
