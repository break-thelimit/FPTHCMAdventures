using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.InventoryDto
{
    public abstract class BaseInventoryDto
    {
        private Guid playerId;

        [Required]
        public Guid PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }
    }
}
