﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.NPCDto
{
    public class GetNpcDto : BaseNpcDto, IBaseDto
    {
        public Guid Id { get ; set ; }
        public string QuestionName { get; set; }
    }
}
