using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Dtos.SchoolEventDto
{
    public class CreateListSchoolEventDto
    {
        private Guid eventId;
        private List<Guid> schoolIds;
        private string status;
        private DateTime startTime;
        private DateTime endTime;
        private string approvalstatus;

        [Required]
        public Guid EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        [Required]
        public List<Guid> SchoolIds
        {
            get { return schoolIds; }
            set { schoolIds = value; }
        }


        [Required]
        [RegularExpression("^(ACTIVE|INACTIVE)$", ErrorMessage = "Status must be 'ACTIVE' or 'INACTIVE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        [Required]
        [RegularExpression("^(ACCEPT|REFUSE)$", ErrorMessage = "Status must be 'ACCEPT' or 'REFUSE'.")]
        public string Approvalstatus
        {
            get { return approvalstatus; }
            set { approvalstatus = value; }
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
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}
