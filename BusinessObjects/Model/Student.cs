using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Student
    {
        public Student()
        {
            Players = new HashSet<Player>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public long Phonenumber { get; set; }
        public string GraduateYear { get; set; }
        public string Classname { get; set; }
        public string Status { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
