using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.SchoolEventDto
{
    public abstract class BaseSchoolEventDto
    {
        private Guid eventId;
        private Guid schoolId;
        private string invitationLetter;
        private string status;

        [Required]
        public Guid EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        [Required]
        public Guid SchoolId
        {
            get { return schoolId; }
            set { schoolId = value; }
        }

        [Required]
        public string InvitationLetter
        {
            get { return invitationLetter; }
            set { invitationLetter = value; }
        }

        [RegularExpression("^(ACCEPT|REFUSE)$", ErrorMessage = "Status must be 'ACCEPT' or 'REFUSE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
