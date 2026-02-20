using UnityEngine;

public class Crop : MonoBehaviour
{
    private SeedData seedData;
    private int currentStage = 0;
    private float timer = 0f;

    private GameObject currentVisual;

    public void Initialize(SeedData data)
    {
        seedData = data;
        GrowToStage(0);
    }

    void Update()
    {
        if (seedData == null) return;

        timer += Time.deltaTime;

        if (currentStage < seedData.growthStageDurations.Length)
        {
            if (timer >= seedData.growthStageDurations[currentStage])
            {
                timer = 0f;
                currentStage++;

                if (currentStage < seedData.growthStagePrefabs.Length)
                    GrowToStage(currentStage);
            }
        }
    }

    void GrowToStage(int stage)
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        currentVisual = Instantiate(seedData.growthStagePrefabs[stage], transform);
    }

    public void Harvest()
    {
        if (currentStage >= seedData.growthStagePrefabs.Length - 1)
        {
            Instantiate(seedData.harvestItemPrefab, transform.position, Quaternion.identity);

            if (seedData.regrow)
            {
                currentStage--;
                timer = -seedData.regrowTime;
                GrowToStage(currentStage);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}