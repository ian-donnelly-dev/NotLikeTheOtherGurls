using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    
    [SerializeField] private float maxAngle = 85f;
    [SerializeField] private float walkSpeed = 10f;
    
    private PlayerInputActionsClass playerInputActionsClass;

    private CharacterController _controller;
    
    private float rotation;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        playerInputActionsClass = new PlayerInputActionsClass();
        
        playerInputActionsClass.MainMovement.Enable();
        
        playerInputActionsClass.MainMovement.Jump.performed += Jump;

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 look = playerInputActionsClass.MainMovement.Look.ReadValue<Vector2>() * Time.deltaTime;
        
        rotation -= look.y;
        rotation = Mathf.Clamp(rotation, -maxAngle, maxAngle);
        
        Vector2 movement = playerInputActionsClass.MainMovement.Movement.ReadValue<Vector2>().normalized;
        Vector3 movementVector = transform.right * movement.x + transform.forward * movement.y;

        _camera.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.x);
        _controller.Move(movementVector * (walkSpeed * Time.deltaTime));
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
}
