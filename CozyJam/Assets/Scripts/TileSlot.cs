using UnityEngine;

public class TileSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public Crop plantedCrop;

    public void TryPlantSeed(CropData seedData, PlayerController player)
    {
        if (isOccupied) return;

        if (!SeedInventory.Instance.UseSeed(seedData))
        {
            Debug.Log("No seeds left!");
            return;
        }

        GameObject cropObj = Instantiate(seedData.growingPrefab, transform.position, Quaternion.identity);

        Crop crop = cropObj.GetComponent<Crop>();
        crop.Initialize(seedData);
        crop.tileSlot = this;

        plantedCrop = crop;
        isOccupied = true;

        player.SetNormalState();
    }

    public void TryRemoveCrop()
    {
        if (!isOccupied || plantedCrop == null)
        {
            Debug.Log("Nothing to remove");
            return;
        }

        Destroy(plantedCrop.gameObject);

        plantedCrop = null;
        isOccupied = false;

        Debug.Log("Crop removed");

    }
}