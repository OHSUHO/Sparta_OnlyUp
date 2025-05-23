using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<InventorySlot> slots = new List<InventorySlot>();
    private bool isShow = false;
    private ItemData _selectedItemData;
    public int selectedSlotIndex;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemValue;
    [SerializeField] private Button useButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unEquipButton;
    [SerializeField] private Button throwButton;
    
    private void Awake()
    {
        InventorySlot[] findedSlots = FindObjectsByType<InventorySlot>(FindObjectsInactive.Include,FindObjectsSortMode.InstanceID);
        for (int i = 0; i < findedSlots.Length; i++)
        {
            findedSlots[i].index = i;
            findedSlots[i].SetInventory(this);
            slots.Add(findedSlots[i]);
            
        }
        InitInventory();
        
    }

    private void InitInventory()
    {
        _selectedItemData = null;
        itemName.gameObject.SetActive(false);
        itemDesc.gameObject.SetActive(false);
        itemValue.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unEquipButton.gameObject.SetActive(false);
        throwButton.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        Utility.TryAssign(ref itemName, "ItemName");
        Utility.TryAssign(ref itemDesc, "ItemDescription");
        Utility.TryAssign(ref itemValue, "ItemValue");
        Utility.TryAssign(ref useButton, "UseButton");
        Utility.TryAssign(ref equipButton, "EquipButton");
        Utility.TryAssign(ref unEquipButton, "UnEquipButton");
        Utility.TryAssign(ref throwButton, "ThrowButton");
    }

    public void UpdateInventory()
    {
        foreach (InventorySlot slot in slots)
        {
                slot.SetSlot();
        }
    }
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OpenInventory();
            CharacterManager.Instance.Player.PlayerController.ToggleCanLook(isShow);
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
    public void SelectItem(ItemData itemdata)
    {  
        UpdateInventory();
        if (itemdata != null)
        {
            _selectedItemData = itemdata;
            equipButton.gameObject.SetActive(_selectedItemData.type==ItemType.Equipable);
            unEquipButton.gameObject.SetActive(_selectedItemData.type==ItemType.Equipable);
            useButton.gameObject.SetActive(_selectedItemData.type==ItemType.Consumable);
            throwButton.gameObject.SetActive(true);
            itemName.gameObject.SetActive(true);
            itemDesc.gameObject.SetActive(true);
            itemValue.gameObject.SetActive(true);
            itemName.SetText(_selectedItemData.itemName);
            itemDesc.SetText(_selectedItemData.itemDescription);
            itemValue.SetText(_selectedItemData.statType.ToString() +" +"+ _selectedItemData.itemValue);
        }
        else
        {
            InitInventory();
            this.gameObject.SetActive(true);
        }
        
    }
    
    
    public void OpenInventory()
    {
        isShow = !isShow;
        selectedSlotIndex = -1;
        if (isShow)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnClickUseButton()
    {
        if (_selectedItemData.type == ItemType.Consumable)
        {
            ConsumableObject obj = _selectedItemData.consumablePrefab.GetComponent<ConsumableObject>();
            obj.Consume();
            slots[selectedSlotIndex].ClearSlot();
            InitInventory();
            gameObject.SetActive(true);
        }
    }
    
}
