using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActionsClass playerInputActionsClass;
    private void Awake()
    {
        playerInputActionsClass = new PlayerInputActionsClass();
        
        playerInputActionsClass.MainMovement.Enable();
        
        playerInputActionsClass.MainMovement.Jump.performed += Jump;
    }

    private void Update()
    {
        Debug.Log(playerInputActionsClass.MainMovement.Look.ReadValue<Vector2>()); 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump" + context.phase);
    }
}
