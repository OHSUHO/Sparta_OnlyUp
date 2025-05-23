using UnityEngine;

[CreateAssetMenu(menuName = "New Item",fileName ="ItemData")]
public class ItemData : ScriptableObject
{
   public string itemName;
   public string itemDescription;
   public ItemType type;
   public Sprite itemIcon;
   public GameObject equipPrefab;
   public GameObject consumablePrefab;
   public ItemStat statType;
   public float itemValue;
   public float time;

}

public  enum ItemType
{
   Environment,
   Item,
   Consumable,
   Equipable,
   
}

public enum ItemStat
{
   AttackStat,
   DefenseStat,
   HealthStat,
   
}

public interface IConsumable
{
   public void Consume();
   
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