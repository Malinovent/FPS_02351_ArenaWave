using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private PlayerCamera playerCamera;

    InputPlayer controls;

    private bool isActive = true;

    private Vector2 moveInput;
    private Vector2 lookInput;

    void Awake()
    {
        controls = new InputPlayer();
        controls.Enable();

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        controls.Player.Look.performed += OnLook;
        controls.Player.Look.canceled += OnLook;

        controls.Player.Jump.performed += OnJump;
    }

    void Update()
    {
        if (!isActive)
            return;
        
        playerMotor.UpdateMotor();
        playerMotor.Move(moveInput);

        playerMotor.Rotate(lookInput.x);
        playerCamera.Rotate(lookInput.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        playerMotor.Jump();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

}
