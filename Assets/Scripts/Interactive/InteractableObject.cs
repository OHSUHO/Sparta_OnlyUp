using System.Text;
using UnityEngine;
public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]private ItemData _data;
    public ItemData data => _data;
    

    public virtual string GetObjectInfoWithString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_data.itemName);
        sb.Append("\n\n");
        sb.Append(_data.itemDescription);
        sb.Append("\n");

        if (_data.type == ItemType.Environment)
        {
            sb.Append("환경 오브젝트");
        }
        else
        {
            sb.Append("'E'키를 눌러 습득가능");
        }
        return sb.ToString();
    }

    public virtual ItemType GetItemType()
    {
        return _data.type;
    }
    
    
    
}
