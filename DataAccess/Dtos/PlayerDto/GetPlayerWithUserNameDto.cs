using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos.PlayerDto
{
    public class GetPlayerWithUserNameDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? TotalPoint { get; set; }
        public int? TotalTime { get; set; }

        public string NickName { get; set; }
    }
}
