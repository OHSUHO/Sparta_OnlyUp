using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData _data;
    private Outline _outline;
    private Image _image;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _image = GetComponent<Image>();
    }

    public ItemData data
    {
        get => _data; set=>_data=value;
    }

    public void ChangeSprite()
    {
        if (_data.itemIcon == null)
        {
            _data.itemIcon = Resources.Load<Sprite>("Sprites/"+_data.itemName);
        }

        if (data.itemIcon != null)
        {
            _image.sprite = data.itemIcon;
        }
    }
    


}
