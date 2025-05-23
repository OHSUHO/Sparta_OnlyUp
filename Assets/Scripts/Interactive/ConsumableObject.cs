using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConsumableObject : InteractableObject,IConsumable
{
        

    public void Consume()
    {
        if (ItemStat.HealthStat == base.data.statType)
        {
            CharacterManager.Instance.Player.PlayerCondition.Heal(base.data.itemValue,base.data.time);
        }
        
        
    }
}
