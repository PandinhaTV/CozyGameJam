using UnityEngine;

public class CropPlace : MonoBehaviour
{
    public bool isOccupied = false;
    private GameObject currentCrop;

    public GameObject cropBasePrefab;

    public void Plant(SeedData seed)
    {
        if (isOccupied) return;

        GameObject cropObject = Instantiate(cropBasePrefab, transform.position, Quaternion.identity);
        Crop crop = cropObject.GetComponent<Crop>();
        crop.Initialize(seed);
        Debug.Log("Crop Created");
        currentCrop = cropObject;
        isOccupied = true;
    }
}