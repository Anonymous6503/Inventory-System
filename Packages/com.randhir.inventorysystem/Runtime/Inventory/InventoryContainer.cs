using System;
using System.Collections.Generic;
using UnityEngine;
using Randhir.InventorySystem.Items;

namespace Randhir.InventorySystem.Inventory
{
    [System.Serializable]
    public class InventoryContainer
    {
        [SerializeField] private int slotCount = 20;
        [SerializeField] private List<InventorySlot> slots;

        public event Action OnInventoryChanged;

        public IReadOnlyList<InventorySlot> Slots => slots;

        public InventoryContainer(int slotCount)
        {
            this.slotCount = slotCount;
            slots = new List<InventorySlot>(slotCount);
            for (int i = 0; i < slotCount; i++)
                slots.Add(new InventorySlot(null, 0));
        }

        public void SetInventory()
        {
            if (slots == null || slots.Count != slotCount)
            {
                slots = new List<InventorySlot>(slotCount);
                for (int i = 0; i < slotCount; i++)
                    slots.Add(new InventorySlot(null, 0));
            }
        }

        public bool AddItem(BaseItem item, int amount = 1)
        {
            if (item == null) return false;

            // 1. Try to stack in existing slot
            foreach (var slot in slots)
            {
                if (slot.CanStack(item))
                {
                    amount = slot.AddToStack(amount);
                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }

            // 2. Add to empty slot
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item, amount);
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }

            return false; // No space left
        }

        public bool RemoveItem(BaseItem item, int amount = 1)
        {
            if (item == null) return false;

            int totalRemoved = 0;

            foreach (var slot in slots)
            {
                if (slot.Item == item)
                {
                    int removed = slot.RemoveFromStack(amount - totalRemoved);
                    totalRemoved += removed;

                    if (totalRemoved >= amount)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }

            return false; // Not enough to remove
        }

        public void Clear()
        {
            foreach (var slot in slots)
                slot.ClearSlot();

            OnInventoryChanged?.Invoke();
        }
    }
}
