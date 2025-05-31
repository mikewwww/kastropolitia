using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PickupItem : MonoBehaviour
{
    public Sprite itemIcon;
    public string itemName;
    public string itemDescription;

    public CanvasGroup hintCanvasGroup;      // Fade στο UI hint
    public TMP_Text hintText;                // Π.χ. "Πάτησε E για να σηκώσεις Ξίφος"

    private bool playerInRange = false;
    private bool fading = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (hintCanvasGroup != null && hintText != null && !fading)
            {
                hintText.text = $"[E] {itemName}\n<size=75%>{itemDescription}</size>";
                StartCoroutine(FadeHint(1f));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (hintCanvasGroup != null && !fading)
                StartCoroutine(FadeHint(0f));
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inv = FindObjectOfType<InventoryManager>();
            if (inv != null)
            {
                bool added = inv.AddItem(itemIcon);
                if (added)
                {
                    if (hintCanvasGroup != null && !fading)
                        StartCoroutine(FadeHint(0f));

                    Destroy(gameObject);
                }
                else
                {
                    if (hintText != null)
                        hintText.text = "<color=red>Το Inventory είναι γεμάτο</color>";
                }
            }
        }
    }

    IEnumerator FadeHint(float targetAlpha)
    {
        fading = true;
        float duration = 0.25f;
        float startAlpha = hintCanvasGroup.alpha;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            hintCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            yield return null;
        }

        hintCanvasGroup.alpha = targetAlpha;
        fading = false;
    }
}
