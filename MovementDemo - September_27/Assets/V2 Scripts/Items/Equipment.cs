using UnityEngine;

public enum EquipType { HELMET, CHEST, ARM, WEAPON, LEG, ARTIFACT } 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipType equipType;
 
    // ask nathan how our stat modifications work
    public int vitModifier;
    public int strModifier;
    public int endModifier;
    public int wisModifier;
    public int fortModifier;
    public int dexModifier;
    public int agilModifier;
 
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        InventoryV2.instance.RemoveItem(this);
    }

}