using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Dtos.EventTaskDto
{
    public class CreateListEventTaskDto
    {
        private List<Guid> taskId;
        private Guid eventId;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private int priority;
        private double point;
        private string status;

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<Guid> TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }

        [Required]
        public Guid EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        [Required]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [Required]
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        [Required]
        public double Point
        {
            get { return point; }
            set { point = value; }
        }
        [RegularExpression("^(INACTIVE|ACTIVE)$", ErrorMessage = "Status must be 'INACTIVE' or 'ACTIVE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
