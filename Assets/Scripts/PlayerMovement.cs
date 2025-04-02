using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // ���������� �� �� �������
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
        float moveZ = Input.GetAxis("Vertical"); // ���� �������-����
        float rotate = Input.GetAxis("Horizontal"); // ���������� �����-��������
        bool isWalking = animator.GetBool("isWalking");
        

        // ���������� �� ����� ������
        if (Mathf.Abs(rotate) > 0.1f)
        {
            Quaternion rotation = Quaternion.Euler(0, rotate * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= rotation;
        }

        // ������ ���� �������-����, ���� ���������� ��� ������� � �������
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
