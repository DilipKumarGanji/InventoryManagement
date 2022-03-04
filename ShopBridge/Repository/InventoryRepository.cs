using ShopBridge.Data;
using ShopBridge.Models;
using ShopBridge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _db;
        public InventoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool AddItemToInventory(Inventory inventoryItem)
        {
            _db.inventory.Add(inventoryItem);
            return save();
        }

        public bool DeleteItemFromInventory(Inventory itemToDelete)
        {
            _db.inventory.Remove(itemToDelete);
            return save();
        }

        public ICollection<Inventory> GetAllInventoryItems()
        {
            return _db.inventory.OrderBy(a => a.ItemID).ToList();
        }

        public Inventory GetInventoryItem(int ItemId)
        {
            return _db.inventory.FirstOrDefault(a => a.ItemID == ItemId);

        }

        public bool InventoryItemExists(int ItemId)
        {
            bool value = _db.inventory.Any(a => a.ItemID== ItemId);
            return value;
        }

        public bool save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateItemInInventory(Inventory inventoryItem)
        {
            _db.inventory.Update(inventoryItem);
            return save();
        }
    }
}
