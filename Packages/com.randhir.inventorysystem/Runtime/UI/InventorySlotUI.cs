using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Randhir.InventorySystem.Items;

namespace Randhir.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantityText;

        private BaseItem item;
        private int quantity;

        public void Set(BaseItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;

            if (item != null)
            {
                icon.enabled = true;
                icon.sprite = item.icon;
                quantityText.text = quantity > 1 ? quantity.ToString() : "";
            }
            else
            {
                icon.enabled = false;
                quantityText.text = "";
            }
        }
    }
}
