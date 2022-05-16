using UnityEngine;

namespace Kira
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item", order = 0)]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }

    public class ItemData
    {
        public string itemName;
        public Sprite icon;

        public ItemData(Item item)
        {
            itemName = item.itemName;
            icon = item.icon;
        }
    }
}