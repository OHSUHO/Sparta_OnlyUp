using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData _data;
    private Button _button;
    private Outline _outline;
    private Image _image;
    private Inventory _inventory;
    public int index;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public ItemData data
    {
        get => _data; set=>_data=value;
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory=inventory;
    }
    public void ChangeSprite()
    {
        if (_data == null) return;
        if (_data.itemIcon == null)
        {
            _data.itemIcon = Resources.Load<Sprite>("Sprites/"+_data.itemName);
        }

        if (data.itemIcon != null)
        {
            _image.sprite = data.itemIcon;
        }
    }

    public void SetOutline()
    {
        if (_inventory.selectedSlotIndex == index)
        {
            _outline.enabled = true;
        }
        else
        {
            _outline.enabled = false;
        }
    }

    public void OnClickSlot()
    {
        _inventory.selectedSlotIndex = index;
        _inventory.SelectItem(_data);
    }

    public void SetSlot()
    {
        ChangeSprite();
        SetOutline();
    }

    public void ClearSlot()
    {
        _data = null;
        _image.sprite = null;
        _outline.enabled = false;
    }
    
    


}
