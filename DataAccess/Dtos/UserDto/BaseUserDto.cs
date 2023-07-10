﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.UserDto
{
    public abstract class BaseUserDto
    {
        public Guid? SchoolId { get; set; }
        public Guid? RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public string Status { get; set; }
    }
}
