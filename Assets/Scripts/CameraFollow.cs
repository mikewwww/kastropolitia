using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ο παίκτης που ακολουθεί η κάμερα
    public Vector3 offset = new Vector3(0, 5, -7); // Θέση της κάμερας σε σχέση με τον παίκτη
    public float smoothSpeed = 5f; // Ταχύτητα ομαλής κίνησης
    public float rotationSpeed = 5f; // Ταχύτητα ομαλής περιστροφής

    void LateUpdate()
    {
        if (target == null) return;

        // 1️⃣ Ομαλή μετακίνηση
        Vector3 desiredPosition = target.position + target.transform.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 2️⃣ Ομαλή περιστροφή
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
