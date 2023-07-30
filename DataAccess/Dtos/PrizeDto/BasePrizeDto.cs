using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PrizeDto
{
    public abstract class BasePrizeDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

    }
}
