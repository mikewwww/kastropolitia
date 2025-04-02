using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 8f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.2f; // Πόσο μακριά θα ελέγχει για έδαφος
    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Χρησιμοποιούμε Raycast για να ανιχνεύουμε το έδαφος
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("isJumping", false); // Ενημέρωση όταν ακουμπά το έδαφος
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetBool("isJumping", true); // Ενημέρωση όταν πηδάει
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
