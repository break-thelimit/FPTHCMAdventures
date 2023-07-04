using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.ItemDto
{
    public abstract class BaseItemDto
    {
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public int? Quantity { get; set; }
    }
}
