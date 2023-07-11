﻿using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.Users
{
    public class UserWithToken : User
    {
        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    public UserWithToken(User user)
    {
        this.Id = user.Id;
        this.Email = user.Email;
        this.School = user.School;
        this.PhoneNumber = user.PhoneNumber;
        this.Fullname = user.Fullname;
        this.Gender = user.Gender;

        this.Role = user.Role;
    }
}
}