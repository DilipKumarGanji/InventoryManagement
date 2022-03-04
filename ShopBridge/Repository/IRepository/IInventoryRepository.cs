using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repository.IRepository
{
    public interface IInventoryRepository
    {
        ICollection<Inventory> GetAllInventoryItems();
        Inventory GetInventoryItem(int ItemId);

        bool InventoryItemExists(int ItemId);

        bool AddItemToInventory(Inventory inventoryItem);

        bool UpdateItemInInventory(Inventory inventoryItem);

        bool DeleteItemFromInventory(Inventory itemToDelete);

        bool save();
    }
}
