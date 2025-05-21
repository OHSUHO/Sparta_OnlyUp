using TMPro;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
    private ItemData _curItemData;
    private Camera _camera;
    [SerializeField]LayerMask interactableLayer;
    [SerializeField] [Range(1, 10)] private float distanceInteract = 3f;
    private float _lastInteractTime;
    private float _interactRate = 0.3f;

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
            UIManager.Instance.SetPrompt(hit.transform.name);
        }

        else
        {
            UIManager.Instance.ClearPrompt();
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
