﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public class UpdatePlayerDto  : BasePlayerDto, IBaseDto
    {
        public Guid Id { get; set; }
}
}
