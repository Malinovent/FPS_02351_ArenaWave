using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private PlayerCamera playerCamera;

    [SerializeField] private WeaponBase[] weapons;
    private int weaponIndex = 0;

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

        controls.Player.Crouch.performed += OnCrouch;

        controls.Player.Sprint.performed += OnSprintStart;
        controls.Player.Sprint.canceled += OnSprintEnd;

        controls.Player.Jump.performed += OnJump;

        controls.Player.Fire.performed += OnFirePressed;
        controls.Player.Fire.canceled += OnFireReleased;
        controls.Player.Reload.performed += OnReload;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isActive)
            return;
        
        playerMotor.UpdateMotor();
        playerMotor.Move(moveInput);

        playerMotor.Rotate(lookInput.x);
        playerCamera.Rotate(lookInput.y);

        weapons[weaponIndex].UpdateWeapon();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
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

    private void OnSprintStart(InputAction.CallbackContext context)
    {
        playerMotor.SprintStart();
    }

    private void OnSprintEnd(InputAction.CallbackContext context)
    {
        playerMotor.SprintEnd();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        playerMotor.ToggleCrouch();
    }

    private void OnFirePressed(InputAction.CallbackContext context)
    {
        weapons[weaponIndex].OnFirePressed();
    }

    private void OnFireReleased(InputAction.CallbackContext context)
    {
        weapons[weaponIndex].OnFireReleased();
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        weapons[weaponIndex].OnReload();
    }



}
