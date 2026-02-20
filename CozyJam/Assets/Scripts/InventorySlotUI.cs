using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData item;
    public Image icon;

    private GameObject dragIcon;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return;

        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(transform.root);

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = item.icon;
        img.raycastTarget = false;

        dragIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            dragIcon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);

        // Se largou fora da UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SpawnInWorld();
        }
    }

    void SpawnInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(item.worldPrefab, hit.point, Quaternion.identity);
        }
    }
}