using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.TaskDto
{
    public class GetTaskByEventIdDto
    {
        public string EventName { get; set; }
        public Guid TaskId { get; set; }
        public Guid EventtaskId { get; set; }
        public string LocationName { get; set; }
        public string MajorName { get; set; }
        public Guid MajorId { get; set; }
        public string NpcName { get; set; }
        public string ItemName { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? Priority { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
    }
}
