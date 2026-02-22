using System;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image icon;
    public ItemData item;
    public TMPro.TMP_Text text;

    private void Update()
    {
       // text.text = item.quantity.ToString();
    }

    

    public void SetItem(ItemData newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Clear()
    {
        item = null;
        icon.enabled = false;
    }
}