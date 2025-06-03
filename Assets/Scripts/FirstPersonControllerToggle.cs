using UnityEngine;

public class FirstPersonControllerToggle : MonoBehaviour
{
    public GameObject crosshairDot;
    public bool isFirstPerson = true;

    void Start()
    {
        UpdateCursorState();
    }

    void Update()
    {
        // Προσωρινός έλεγχος για εναλλαγή προβολής
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            UpdateCursorState();
        }
    }

    void UpdateCursorState()
    {
        if (isFirstPerson)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (crosshairDot != null)
                crosshairDot.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (crosshairDot != null)
                crosshairDot.SetActive(false);
        }
    }
}
