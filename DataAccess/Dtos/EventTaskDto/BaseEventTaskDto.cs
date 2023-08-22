using System;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Model;

namespace DataAccess.Dtos.EventTaskDto
{
    public abstract class BaseEventTaskDto
    {
        private Guid taskId;
        private Guid eventId;
        private DateTime startTime;
        private DateTime endTime;
        private int? priority;
        private double point;

        [Required]
        public Guid TaskId
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
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [Required]
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [Required]
        public int? Priority
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
    }
}
