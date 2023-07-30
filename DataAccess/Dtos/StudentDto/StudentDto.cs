using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.StudentDto
{
    public class StudentDto
    {
        public string Schoolname { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public long Phonenumber { get; set; }
        public string GraduateYear { get; set; }
        public string Classname { get; set; }
        public string Status { get; set; }
    }
}
