using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public HotbarSlot[] slots;
    public int selectedIndex = 0;
    public SeedData[] items;
    public GameObject[] selectedObject;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            UseSelectedItem();
        }
    }

    public void AddItem(int index)
    {
        slots[index].item.quantity++;
    }

    public void SelectSlot(int index)
    {
        selectedIndex = index;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.localScale = (i == index)
                ? Vector3.one * 1.2f
                : Vector3.one;
        }
    }

    void UseSelectedItem()
    {

        RaycastHit hit = CastRay();
        if (hit.collider != null && hit.collider.CompareTag("CropPlace"))
        {
            CropPlace cropPlace = hit.collider.GetComponent<CropPlace>();
            if (cropPlace != null && !cropPlace.isOccupied)
            {
                cropPlace.Plant(selectedObject[selectedIndex].GetComponent<SeedReference>().seedData);

            }
        }
    }




private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}