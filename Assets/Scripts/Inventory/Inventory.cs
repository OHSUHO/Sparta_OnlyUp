using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    List<InventorySlot> slots = new List<InventorySlot>();
    private bool isShow = false;
    
    private void Awake()
    {
        InventorySlot[] findedSlots = FindObjectsOfType<InventorySlot>(includeInactive:true);
        for (int i = 0; i < findedSlots.Length; i++)
        {
            slots.Add(findedSlots[i]);
        }
    }

    public void UpdateInventory()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.data != null)
            {
               slot.ChangeSprite();
            }
        }
    }
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isShow = !isShow;
            CharacterManager.Instance.Player.PlayerController.ToggleCanLook(isShow);
            if (isShow)
            {
                UIManager.Instance.ShowInventory();
            }
            else
            {
                UIManager.Instance.InitInventory();
            }
            
        }
    }
    public void GetItem(ItemData item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.data == null)
            {
                slot.data = item;
                break;
            }
        }
    }
    
    
}
