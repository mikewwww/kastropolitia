using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 8f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.2f; // Πόσο μακριά θα ελέγχει για έδαφος

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Χρησιμοποιούμε Raycast για να ανιχνεύουμε το έδαφος
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
