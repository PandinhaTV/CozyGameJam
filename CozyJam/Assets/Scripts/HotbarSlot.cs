using System;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image icon;
    public SeedData item;
    public TMPro.TMP_Text text;
    
    private void Update()
    {
        text.text = item.quantity.ToString();
    }

    public void Start()
    {
        
        gameObject.GetComponent<Image>().sprite = item.icon;
        
    }

    
    

    public void Clear()
    {
        item = null;
        icon.enabled = false;
    }
}