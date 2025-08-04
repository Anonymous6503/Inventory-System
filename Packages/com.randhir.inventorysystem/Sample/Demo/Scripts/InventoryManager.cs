using UnityEngine;
using Randhir.InventorySystem.Inventory;
using Randhir.InventorySystem.Items;
using Randhir.InventorySystem.UI;

namespace Randhir.InventorySystem.Samples
{
    public class InventoryManager : MonoBehaviour
    {
        public BaseItem testItem;
        public int slotCount = 10;

        [SerializeField]private InventoryContainer inventory;

        public InventoryUI inventoryUI;

        private void Awake()
        {
            inventory = new InventoryContainer(slotCount);
            inventory.SetInventory();
            inventory.OnInventoryChanged += OnInventoryChanged;

            if (inventoryUI != null)
                inventoryUI.SetInventory(inventory);
        }

        [ContextMenu("Add Item")]
        public void AddTestItem()
        {
            if (inventory.AddItem(testItem))
                Debug.Log("Item added!");
            else
                Debug.Log("Failed to add item.");
        }

        [ContextMenu("Remove Item")]
        public void RemoveTestItem()
        {
            if (inventory.RemoveItem(testItem))
                Debug.Log("Item removed!");
            else
                Debug.Log("Item not found.");
        }

        private void OnInventoryChanged()
        {
            Debug.Log("Inventory changed.");
            inventoryUI?.RefreshUI();
        }
    }
}
