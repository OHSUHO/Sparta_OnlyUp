using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(1, 10)] private float moveSpeed;
    [SerializeField] [Range(1, 10)] private float jumpForce;

    [Header("Look")] 
    private Vector2 _mouseDelta;
    [SerializeField]private Transform cameraTransform;
    private float _cursorXposition;
    private float _cursorYposition;
    private float _minY = -80f;
    private float _maxY = 80f;
    private bool _canLook = true;

    private Rigidbody _rigidbody;
    private Vector2 _curMovementInput;
    
    [SerializeField] LayerMask groundMask;

    private void Awake()
    {
        if (!TryGetComponent(out _rigidbody))
        {
            Debug.LogWarning("No rigidbody attached");
        }

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move(_curMovementInput);
        if (_canLook)
        {
          OnLook();
        }
    }

    private void Move(Vector2 input)
    {
        Vector3 dir = transform.forward * input.y + transform.right * input.x;
        dir  =  dir * moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void ToggleCanLook(bool isBool)
    {
        _canLook = !isBool;
        Cursor.lockState = !isBool ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
           _curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }
        
    }



    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CheckGround())
        {
            Jump();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    private void OnLook()
    {
        _cursorXposition += _mouseDelta.x;
        _cursorYposition -= _mouseDelta.y;
        _cursorYposition = Mathf.Clamp(_cursorYposition, _minY, _maxY);
        cameraTransform.localEulerAngles = new Vector2(_cursorYposition, 0);
        transform.eulerAngles = new Vector2(0, _cursorXposition);
    }


    private bool CheckGround()
    {
        Ray[] rays = new Ray[]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)

        };
        for (int i = 0; i < rays.Length; i++)
        {
            
            if (Physics.Raycast(rays[i], 1f, groundMask))
            {
                return true;
            }
        }
        return false;
        
    }
    
    
    
}
