using System.Collections.Concurrent;

namespace BConsoleApp
{
    public class Warehouse
    {
        private BlockingCollection<Item> inventory = new BlockingCollection<Item>();
        
        /// <summary>
        /// Add Item To Inventory
        /// </summary>
        /// <param name="i"></param>
        public bool AddItemToInventory(Item i)
        {
            if(!inventory.IsAddingCompleted)
                inventory.TryAdd(i,500);
            
            // Cant add we are done
            return inventory.IsCompleted;
            
        }

        /// <summary>
        /// Take Item From Inventory 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Item TakeItemFromInventory(out bool status,int timeout)
        {
            status = this.inventory.TryTake(out Item item, timeout);
            return item;
        }

        public void EndLoading()
        {
            inventory.CompleteAdding();
        }

        public bool EndProcessing
        {
            get
            {
                return inventory.IsCompleted;
            }
        }
    }
}
