﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.QuestionDto
{
    public class QuestionDto : BaseQuestionDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
