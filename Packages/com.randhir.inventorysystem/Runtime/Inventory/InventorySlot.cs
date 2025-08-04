using System;
using UnityEngine;
using Randhir.InventorySystem.Items;

namespace Randhir.InventorySystem.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private BaseItem item;
        [SerializeField] private int quantity;

        public InventorySlot(BaseItem item, int quantity = 1)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public BaseItem Item => item;
        public int Quantity => quantity;

        public bool IsEmpty => item == null;

        public void SetItem(BaseItem newItem, int newQuantity = 1)
        {
            item = newItem;
            quantity = Mathf.Clamp(newQuantity, 0, newItem != null ? newItem.maxStack : 0);
        }

        public void ClearSlot()
        {
            item = null;
            quantity = 0;
        }

        public bool CanStack(BaseItem otherItem)
        {
            return item != null && item == otherItem && quantity < item.maxStack;
        }

        public int AddToStack(int amount)
        {
            if (item == null) return amount;

            int spaceLeft = item.maxStack - quantity;
            int addAmount = Mathf.Min(spaceLeft, amount);
            quantity += addAmount;
            return amount - addAmount; // Return leftover amount
        }

        public int RemoveFromStack(int amount)
        {
            int removeAmount = Mathf.Min(quantity, amount);
            quantity -= removeAmount;

            if (quantity <= 0)
                ClearSlot();

            return removeAmount;
        }
    }
}
