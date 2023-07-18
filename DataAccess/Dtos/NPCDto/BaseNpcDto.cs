﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.NPCDto
{
    public abstract class BaseNpcDto
    {
        public string NpcName { get; set; }
        public string Introduce { get; set; }
        public Guid? QuestionId { get; set; }
        public string Status { get; set; }
    }
}
