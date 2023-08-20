using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.NPCDto
{
    public abstract class BaseNpcDto
    {
        private string name;
        private string introduce;
        private string status;

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Required]
        public string Introduce
        {
            get { return introduce; }
            set { introduce = value; }
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
