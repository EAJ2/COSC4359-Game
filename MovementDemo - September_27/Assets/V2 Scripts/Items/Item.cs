using UnityEngine;

public enum ItemType { offensive, defensive, equipment, healing }
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public ItemType type;

    public virtual void Use()
    {
        // override (depends on item)
    }

    public virtual void Drop()
    {
        InventoryV2.instance.RemoveItem(this);
    }
}
