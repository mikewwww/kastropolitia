using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // Περιστροφή με τα βελάκια
    private CharacterController controller;
    private Vector3 moveDirection;
    Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical"); // Μόνο μπροστά-πίσω
        float rotate = Input.GetAxis("Horizontal"); // Περιστροφή δεξιά-αριστερά
        bool isWalking = animator.GetBool("isWalking");
        

        // Περιστροφή με ομαλή κίνηση
        if (Mathf.Abs(rotate) > 0.1f)
        {
            Quaternion rotation = Quaternion.Euler(0, rotate * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= rotation;
        }

        // Κίνηση μόνο μπροστά-πίσω, στην κατεύθυνση που κοιτάει ο παίκτης
        moveDirection = transform.forward * moveZ;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
       if(Mathf.Abs(moveZ)> 0.1f){
        animator.SetBool("isWalking", true);
       }
       else
       {
        animator.SetBool("isWalking",false);
       }
    }
}
