using UnityEngine;

public class EquipTemporaryArmor : MonoBehaviour
{
    public string armorName = "πανοπλία";
    public string armorObjectName = "Armor1";
    public Transform armorPartsRoot; // ← Βάλε το ARMOR PARTS εδώ

    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt($"[E] Φόρεσε την {armorName}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            EquipSelectedArmor();
            InteractionPrompt.Instance?.HidePrompt();
        }
    }

    void EquipSelectedArmor()
    {
        if (armorPartsRoot == null)
        {
            Debug.LogWarning("⚠ Δεν έχει οριστεί το αντικείμενο ARMOR PARTS στο Inspector!");
            return;
        }

        foreach (Transform armor in armorPartsRoot)
        {
            armor.gameObject.SetActive(armor.name == armorObjectName);
        }
    }
}
