using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float rotate = Input.GetAxis("Horizontal"); // Αριστερά (-1) / Δεξιά (+1)

        if (Mathf.Abs(rotate) > 0.1f)
        {
            Quaternion rotation = Quaternion.Euler(0, rotate * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= rotation;
        }
    }
}
