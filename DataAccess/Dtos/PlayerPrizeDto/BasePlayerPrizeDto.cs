using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerPrizeDto
{
    public class BasePlayerPrizeDto
    {
        public Guid PrizeId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
