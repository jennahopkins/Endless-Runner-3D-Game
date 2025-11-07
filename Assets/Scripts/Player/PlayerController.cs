using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float jumpDuration = 0.7f;
  

    Vector2 movement;
    bool isJumping = false;
    float jumpTimer = 0f;
    bool isGrounded = true;
    Vector3 startPosition;


    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }


    public void Jump(InputAction.CallbackContext context)
    {
        // start jump on performed action if grounded
        if (context.performed && isGrounded)
        {
            StartJump();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // read movement input
        movement = context.ReadValue<Vector2>();
    }


    void StartJump()
    {
        // set jump state
        isJumping = true;
        isGrounded = false;
        jumpTimer = 0f;
        startPosition = transform.position;
    }

    void HandleJump()
    {
        if (isJumping) // jump has been started
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;

            // parabolic jump calculation
            float height = 4f * jumpHeight * normalizedTime * (1f - normalizedTime);
            transform.position = new Vector3(
                transform.position.x,
                startPosition.y + height,
                transform.position.z
            );

            // Reset jump state when jump is complete
            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                isGrounded = true;
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            }
        } else {
            isGrounded = true;
        }
    }

    void HandleMovement()
    {
        // move the player left/right and forward/backward
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = transform.position + moveDirection * (moveSpeed * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        transform.position = newPosition;
    }
}