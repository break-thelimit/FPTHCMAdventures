using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.EventDto
{
    public abstract class BaseEventDto
    {
        private string name;
        private DateTime startTime;
        private DateTime endTime;
        private string status;

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
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
        [RegularExpression("^(INACTIVE|ACTIVE)$", ErrorMessage = "Status must be 'INACTIVE' or 'ACTIVE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
