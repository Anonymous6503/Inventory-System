using UnityEngine;
using System.Collections.Generic;
using Randhir.InventorySystem.Inventory;

namespace Randhir.InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform slotParent;

        private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();
        private InventoryContainer container;

        public void SetInventory(InventoryContainer container)
        {
            this.container = container;
            container.OnInventoryChanged += RefreshUI;

            // Create slots
            foreach (var slot in container.Slots)
            {
                var slotGO = Instantiate(slotPrefab, slotParent);
                var slotUI = slotGO.GetComponent< InventorySlotUI>();
                slotUIs.Add(slotUI);
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = 0; i < container.Slots.Count; i++)
            {
                var slot = container.Slots[i];
                slotUIs[i].Set(slot.Item, slot.Quantity);
            }
        }
    }
}
