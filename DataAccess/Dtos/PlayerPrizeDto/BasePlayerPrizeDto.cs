using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.PlayerPrizeDto
{
    public class BasePlayerPrizeDto
    {
        private Guid prizeId;
        private Guid playerId;

        [Required]
        public Guid PrizeId
        {
            get { return prizeId; }
            set { prizeId = value; }
        }

        [Required]
        public Guid PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }
    }
}
