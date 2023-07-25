﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.MajorDto
{
    public abstract class BaseMajorDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
