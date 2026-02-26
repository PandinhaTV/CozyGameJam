using UnityEngine;

public class CropPlace : MonoBehaviour
{
    public bool isOccupied = false;
    private GameObject currentCrop;

    public GameObject cropBasePrefab;
    public int posx;
    public int posy;
    
    public void Plant(SeedData seed)
    {
        if (isOccupied) return;
        if (seed.quantity==0) return;
        seed.quantity--;
        GameObject cropObject = Instantiate(cropBasePrefab, transform.position, Quaternion.identity);
        Crop crop = cropObject.GetComponent<Crop>();
        crop.Initialize(seed);
        Debug.Log("Crop Created");
        currentCrop = cropObject;
        Neigbour.instance.playerKey[posx, posy] = seed.seedName;
        isOccupied = true;
    }
}