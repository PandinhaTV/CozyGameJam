using UnityEngine;

public class PlantingSystem : MonoBehaviour
{
    public SeedData selectedSeed;
    public GameObject cropBasePrefab;

    public void Plant(Vector3 position)
    {
        GameObject cropObject = Instantiate(cropBasePrefab, position, Quaternion.identity);

        Crop crop = cropObject.GetComponent<Crop>();
        crop.Initialize(selectedSeed);
    }
}
