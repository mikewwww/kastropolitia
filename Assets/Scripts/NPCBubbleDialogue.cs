using UnityEngine;
using TMPro;

public class NPCBubbleDialogue : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public string dialogueLine = "Γεια σου, ταξιδιώτη!";
    public float displayTime = 5f;  // Αύξησα σε 5 δευτερόλεπτα
    public float maxDisplayDistance = 5f;  // Απόσταση ενεργοποίησης

    private TextMeshProUGUI dialogueText;
    private Transform playerTransform;
    private float timer;
    private bool isDialogueActive = false;

    void Start()
    {
        if (dialogueCanvas != null)
        {
            dialogueText = dialogueCanvas.GetComponentInChildren<TextMeshProUGUI>();
            dialogueCanvas.SetActive(false);
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (!Application.isPlaying || dialogueCanvas == null || playerTransform == null)
            return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= maxDisplayDistance)
        {
            // Ενεργοποίηση διαλόγου
            if (!isDialogueActive)
            {
                dialogueText.text = dialogueLine;
                dialogueCanvas.SetActive(true);
                timer = displayTime;
                isDialogueActive = true;
            }
            else
            {
                // Αν ο παίκτης είναι εντός περιοχής, επαναφέρω timer για να μένει ορατό
                timer = displayTime;
            }

            // Κοιτάει την κάμερα
            if (Camera.main != null)
                dialogueCanvas.transform.forward = Camera.main.transform.forward;
        }
        else
        {
            // Όταν ο παίκτης φεύγει, ξεκινάει το countdown για να σβήσει το διάλογο
            if (isDialogueActive)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    dialogueCanvas.SetActive(false);
                    isDialogueActive = false;
                }
            }
        }
    }
}
