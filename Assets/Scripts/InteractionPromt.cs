using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public static InteractionPrompt Instance;

    [SerializeField] private CanvasGroup promptGroup;
    [SerializeField] private TextMeshProUGUI promptText;
    
    private void Start()
    {
        // Κρύψε με καθυστέρηση 1 frame
        Invoke(nameof(HidePrompt), 0.1f);
    }

    private void Awake()
    {
        Instance = this;
        HidePrompt(); // Start hidden
    }

    public void ShowPrompt(string message)
    {
        promptText.text = message;
        promptGroup.alpha = 1f;
        promptGroup.interactable = true;
        promptGroup.blocksRaycasts = true;
    }

    public void HidePrompt()
    {
        promptGroup.alpha = 0f;
        promptGroup.interactable = false;
        promptGroup.blocksRaycasts = false;
    }
}
