using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public HotbarSlot[] slots;
    public int selectedIndex = 0;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        HandleInput();
        
        if (Input.GetMouseButtonDown(0))
        {
            UseSelectedItem();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
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