using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Move Settings")]
    [SerializeField] private float walkSpeed = 5f;

    private float actualMoveSpeed;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchHeight;

    private bool isCrouched = false;
    private float defaultHeight;

    [Header("Sprint Settings")]
    [SerializeField] private float sprintSpeed;

    private bool isSprinting = false;

    [Header("Jump Settings")]
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private int currentJumps = 0;
    private Vector3 velocity;

    private void Awake()
    {
        defaultHeight = controller.height;
        actualMoveSpeed = walkSpeed;
    }

    public void UpdateMotor()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Handle gravity with a player controller
        if (controller.isGrounded)
        {
            currentJumps = 0;

            if (velocity.y < 0f)
            {
                velocity.y = -2f;
            }
        }
    }

    public void Move(Vector2 input)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        controller.Move(move * actualMoveSpeed * Time.deltaTime);
    }

    #region CROUCH

    public void ToggleCrouch()
    {
        isCrouched = !isCrouched;

        if(isCrouched)
        {
            CrouchStart();
        }
        else
        {
            CrouchEnd();
        }
    }

    private void CrouchStart()
    {
        SprintEnd();
        controller.height = 1;
        actualMoveSpeed = crouchSpeed;
    }

    private void CrouchEnd()
    {
        controller.height = 2;
        actualMoveSpeed = walkSpeed;
    }
    #endregion

    #region SPRINT

    public void SprintEnd()
    {
        if (!isSprinting)
            return;

        isSprinting = false;
        actualMoveSpeed = walkSpeed;

    }

    public void SprintStart()
    {
        isSprinting = true;
        CrouchEnd();
        actualMoveSpeed = sprintSpeed;
    }
    #endregion
    public void Jump()
    {
        if (!controller.isGrounded && currentJumps >= maxJumps)
            return;

        Debug.Log($"Jumping: {currentJumps}");
        // Physics-based jump formula
        currentJumps++;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);      
    }

    public void Rotate(float inputX)
    {
        transform.Rotate(Vector3.up * inputX);
    }
}

