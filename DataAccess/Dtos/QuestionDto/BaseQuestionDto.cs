using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.QuestionDto
{
    public abstract class BaseQuestionDto
    {
        private Guid majorId;
        private string name;
        private string status;

        [Required]
        public Guid MajorId
        {
            get { return majorId; }
            set { majorId = value; }
        }

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [RegularExpression("^(ACTIVE|INACTIVE)$", ErrorMessage = "Status must be 'ACTIVE' or 'INACTIVE'.")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
