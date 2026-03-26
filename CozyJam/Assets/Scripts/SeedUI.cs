using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedUI : MonoBehaviour
{
    public CropData crop;
    public TextMeshProUGUI seedText;
    public Button plantButton;

    void Update()
    {
        int amount = SeedInventory.Instance.GetSeedAmount(crop);

        seedText.text = amount.ToString();

        plantButton.interactable = amount > 0;
    }
}