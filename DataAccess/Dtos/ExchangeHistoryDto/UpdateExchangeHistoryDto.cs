﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ExchangeHistoryDto
{
    public class UpdateExchangeHistoryDto : BaseExchangeHistoryDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
