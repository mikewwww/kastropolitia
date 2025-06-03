using UnityEngine;
using System.Collections.Generic;

public class ShopNPC : MonoBehaviour
{
    public ShopUIManager shopUIManager; // Αντιστοιχεί στο κοινό Shop UI
    public List<ShopItem> myShopItems;  // Τα αντικείμενα που πουλάει αυτός ο NPC

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InteractionPrompt.Instance.HidePrompt();
            shopUIManager.OpenShop(myShopItems);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // ➕ Προσθήκη prompt ανάλογα με τον τύπο NPC
            InteractionPrompt.Instance.ShowPrompt("Πάτα [E] για το κατάστημα");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance.HidePrompt();
        }
    }
}
