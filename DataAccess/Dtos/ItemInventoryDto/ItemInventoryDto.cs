﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ItemInventoryDto
{
    public class ItemInventoryDto : BaseItemInventoryDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
