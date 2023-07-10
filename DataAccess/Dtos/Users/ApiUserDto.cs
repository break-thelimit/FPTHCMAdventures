using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.Users
{
    public class ApiUserDto : LoginDto
    {
        public long? PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public string Status { get; set; }
        public string Fullname { get; set; }

    }
}
