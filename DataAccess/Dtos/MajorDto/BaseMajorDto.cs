using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.MajorDto
{
    public abstract class BaseMajorDto
    {
        private string name;
        private string description;
        private string status;

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Required]
        public string Description
        {
            get { return description; }
            set { description = value; }
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
