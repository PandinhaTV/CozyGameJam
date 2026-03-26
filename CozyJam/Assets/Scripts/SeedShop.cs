using UnityEngine;

public class SeedShop : MonoBehaviour
{
    public void BuySeed(CropData crop)
    {
        if (GameManager.Instance.coins < crop.cost)
        {
            Debug.Log("Not enough coins");
            return;
        }

        GameManager.Instance.coins -= crop.cost;

        SeedInventory.Instance.AddSeeds(crop, 1);

        Debug.Log("Bought seed: " + crop.cropName);
    }
}