using UnityEngine;

[CreateAssetMenu(menuName = "New Item",fileName ="ItemData")]
public class ItemData : ScriptableObject
{
   public string itemName;
   public string itemDescription;
   public ItemType type;
   public Sprite itemIcon;
   public GameObject equipPrefab;
}

public  enum ItemType
{
   Environment,
   Item,
   Consumable,
   IEquipable,
   
}

public interface IConsumable
{
   public void Heal(float value);
   
}

public interface IEquippable
{
   public void Equip();
   public void Unequip();
}

public interface IAttackable
{
   public void Attack();
}