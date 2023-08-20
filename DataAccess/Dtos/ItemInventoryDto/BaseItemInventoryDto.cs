using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.ItemInventoryDto
{
    public abstract class BaseItemInventoryDto
    {
        private Guid inventoryId;
        private Guid? itemId;
        private int? quantity;

        [Required]
        public Guid InventoryId
        {
            get { return inventoryId; }
            set { inventoryId = value; }
        }

        public Guid? ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
