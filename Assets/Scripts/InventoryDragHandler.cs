using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Transform dragLayer;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        dragLayer = GameObject.Find("DragLayer")?.transform;
        if (dragLayer != null)
        {
            transform.SetParent(dragLayer, worldPositionStays: false);

            // Σωστά anchors και μέγεθος για να μη χαλάει η θέση/κλίμακα
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(90, 90); // ή όσο είναι το icon
            rectTransform.localScale = Vector3.one;
        }

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            dragLayer as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out globalMousePos))
        {
            rectTransform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject pointerTarget = eventData.pointerEnter;

        // Αν έγινε drop πάνω σε άλλο slot
        if (pointerTarget != null && pointerTarget.GetComponent<InventorySlotUI>())
        {
            if (pointerTarget.transform.childCount > 0)
            {
                Transform existingItem = pointerTarget.transform.GetChild(0);
                existingItem.SetParent(originalParent);
                existingItem.localPosition = Vector2.zero;
            }

            transform.SetParent(pointerTarget.transform);
        }
        else
        {
            // Αν όχι, επιστροφή στη θέση του
            transform.SetParent(originalParent);
        }

        rectTransform.anchoredPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;
        transform.localScale = Vector3.one;
    }
}
