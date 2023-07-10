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
        public string Name { get; set; }

        
    }
}
