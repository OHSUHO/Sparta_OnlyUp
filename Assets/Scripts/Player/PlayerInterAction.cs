using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInterAction : MonoBehaviour
{
    private ItemData _curItemData;
    private Camera _camera;
    [SerializeField]LayerMask interactableLayer;
    [SerializeField] [Range(1, 10)] private float distanceInteract = 3f;
    private float _lastInteractTime;
    private float _interactRate = 0.3f;

    [SerializeField]private InteractableObject curInteractObject;
    [SerializeField]private InteractableObject lastInteractObject;

    private void Awake()
    {
        _camera  = Camera.main;
        interactableLayer = LayerMask.GetMask("Interactable");
    }


    private void FindInteractiveObject()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,distanceInteract,interactableLayer))
        {
            curInteractObject = hit.transform.gameObject.GetComponent<InteractableObject>();
            if (curInteractObject != null && curInteractObject != lastInteractObject)
            {
                UIManager.Instance.SetPrompt(curInteractObject.GetObjectInfoWithString());
                lastInteractObject = curInteractObject;
            }
        }

        else
        {
            lastInteractObject =null;
            UIManager.Instance.ClearPrompt();
        }
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
         if (curInteractObject.GetItemType() != ItemType.Environment)
         {
             Inventory inv = UIManager.Instance.uiInventoryScript;
             inv.GetItem(curInteractObject.GetComponent<InteractableObject>().data);
             inv.UpdateInventory();
             Destroy(curInteractObject.gameObject);
         }
            
        }


    }

    void Update()
    {
        if (Time.time - _lastInteractTime > _interactRate)
        {
            _lastInteractTime = Time.time;
            FindInteractiveObject();
        }
        
        
        
    }




}
