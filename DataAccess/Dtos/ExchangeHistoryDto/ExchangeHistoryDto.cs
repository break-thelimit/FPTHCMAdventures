﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ExchangeHistoryDto
{
    public class ExchangeHistoryDto : BaseExchangeHistoryDto , IBaseDto
    {
        public Guid Id { get; set; }
    }
}
