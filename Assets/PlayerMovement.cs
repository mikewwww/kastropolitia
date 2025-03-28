using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // ���������� �� �� �������
    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical"); // ���� �������-����
        float rotate = Input.GetAxis("Horizontal"); // ���������� �����-��������

        // ���������� �� ����� ������
        if (Mathf.Abs(rotate) > 0.1f)
        {
            Quaternion rotation = Quaternion.Euler(0, rotate * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= rotation;
        }

        // ������ ���� �������-����, ���� ���������� ��� ������� � �������
        moveDirection = transform.forward * moveZ;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
