using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float maxAngle = 85f;
    [SerializeField] private float walkSpeed = 15f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float groundCheckRad = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private PlayerInputActionsClass playerInputActionsClass;
    private Vector2 movement;
    private Vector2 look;
    private bool jump;
    private bool nowGrounded;

    private Transform myTransform;
    private CharacterController charController;
    float rotation;
    private Vector3 velocity;
    private float gravity;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        playerInputActionsClass = new PlayerInputActionsClass();
        playerInputActionsClass.MainMovement.Enable();

        charController = GetComponent<CharacterController>();

        myTransform = GetComponent<Transform>();
        
        gravity = Physics.gravity.y;
    }

    private void Update() //phi jitter fix
    {
        GetInput();
        
        Move();
        
        Look();

        Gravity();
    }

    private void GetInput()
    {
        look = playerInputActionsClass.MainMovement.Look.ReadValue<Vector2>();
        movement = playerInputActionsClass.MainMovement.Movement.ReadValue<Vector2>().normalized;
        jump = playerInputActionsClass.MainMovement.Jump.ReadValue<float>() > 0.5f;
    }
    
    private void Move()
    {
        Vector3 movementVector = myTransform.right * movement.x + myTransform.forward * movement.y;
        charController.Move(movementVector * (walkSpeed * Time.deltaTime));
    }

    private void Look()
    {
        rotation -= look.y * Time.deltaTime;
        rotation = Mathf.Clamp(rotation, -maxAngle, maxAngle);
        cam.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * (look.x * Time.deltaTime));
    }

    private void Gravity()
    {
        nowGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRad, groundMask);

        if (nowGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (nowGrounded && jump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity *  Time.deltaTime);
    }
}