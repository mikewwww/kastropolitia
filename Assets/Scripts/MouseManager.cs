using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    private bool isFirstPersonMode = false;
    private bool isUIOpen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetUIOpen(bool isOpen)
    {
        isUIOpen = isOpen;
        if (isFirstPersonMode && !isUIOpen)
            LockCursor();
        else
            UnlockCursor();
    }

    public bool IsUIOpen()
    {
        return isUIOpen;
    }

    public void SetFirstPerson(bool isFirstPerson)
    {
        isFirstPersonMode = isFirstPerson;
        if (isFirstPerson && !isUIOpen)
            LockCursor();
        else
            UnlockCursor();
    }
}
