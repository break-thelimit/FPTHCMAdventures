using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.RankDto
{
    public abstract class BaseRankDto
    {
        public Guid? PlayerId { get; set; }
        public string RankNumber { get; set; }
    }
}
