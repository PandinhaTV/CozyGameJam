using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public HotbarSlot[] slots;
    public int selectedIndex = 0;
    public ItemData[] items;
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
        ItemData item = slots[selectedIndex].item;
        if (item == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(item.worldPrefab, hit.point, Quaternion.identity);
        }
    }
}