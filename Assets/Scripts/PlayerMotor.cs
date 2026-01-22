using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private int currentJumps = 0;
    private Vector3 velocity;

    public void UpdateMotor()
    {
        Debug.Log("Updating Player Motor");

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Handle gravity with a player controller
        if (controller.isGrounded)
        {
            currentJumps = 0;
            Debug.Log("Player is groudned");
            if (velocity.y < 0f)
            {
                velocity.y = -2f;
            }
        }
    }

    public void Move(Vector2 input)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

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

