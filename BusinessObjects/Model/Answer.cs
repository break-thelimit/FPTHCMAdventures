using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class Answer
    {
        public Guid Id { get; set; }
        public Guid? QuestionId { get; set; }
        public string Answer1 { get; set; }
        public bool? IsRight { get; set; }

        public virtual Question Question { get; set; }
    }
}
