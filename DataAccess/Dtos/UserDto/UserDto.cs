using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.UserDto
{
    public class UserDto : BaseUserDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string RoleName { get; set; }
    }
}
