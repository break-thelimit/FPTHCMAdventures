using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.RankDto
{
    public  class UpdateRankDto : BaseRankDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}

