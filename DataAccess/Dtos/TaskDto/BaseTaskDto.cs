﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.TaskDto
{
    public abstract class BaseTaskDto
    {
        public Guid? LocationId { get; set; }
        public Guid? MajorId { get; set; }
        public Guid? NpcId { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsRequireitem { get; set; }
        public int? TimeOutAmount { get; set; }
        public string ActivityName { get; set; }
        public int? Point { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
