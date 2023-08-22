using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.ExchangeHistoryDto
{
    public abstract class BaseExchangeHistoryDto
    {
        private Guid playerId;
        private Guid itemId;
        private DateTime exchangeDate;
        private int quantity;

        [Required]
        public Guid PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }

        [Required]
        public Guid ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        [Required]
        public DateTime ExchangeDate
        {
            get { return exchangeDate; }
            set { exchangeDate = value; }
        }

        [Required]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
