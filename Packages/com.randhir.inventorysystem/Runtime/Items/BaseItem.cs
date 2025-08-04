using UnityEngine;

namespace Randhir.InventorySystem.Items
{
    public enum ItemType
    {
        Default,
        CraftingMaterial
    }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory System/Item")]
    public class BaseItem : ScriptableObject
    {
        [Header("Basic Info")]
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;

        [Header("Category")]
        public ItemType itemType;
        public string[] tags;

        [Header("Stacking")]
        public int maxStack = 1;

    }
}
