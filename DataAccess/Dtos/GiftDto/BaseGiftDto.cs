using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.GiftDto
{
    public abstract class BaseGiftDto
    {
        public Guid? RankId { get; set; }
        public string GiftName { get; set; }
        public string Decription { get; set; }
        public int? Price { get; set; }
    }
}
