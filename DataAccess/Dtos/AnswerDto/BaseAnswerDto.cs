using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.AnswerDto
{
    public abstract class BaseAnswerDto
    {
        public Guid? QuestionId { get; set; }
        public string Answer { get; set; }
        public bool? IsRight { get; set; }
    }
}
