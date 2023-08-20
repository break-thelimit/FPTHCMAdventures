using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.AnswerDto
{
    public class AnswerDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string AnswerName { get; set; }
        public bool IsRight { get; set; }
        public string QuestionName { get; set; }
        public Guid? QuestionId { get; set; }

    }
}
