using UnityEngine;
using UnityEngine.InputSystem;

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
                else if  (currentStage >= seedData.growthStagePrefabs.Length)
                {
                    Debug.Log("Ready to harvest");
                    RaycastHit hit = CastRay();
                    if (hit.collider != null && hit.collider == this.gameObject)
                    {
                                            switch (seedData.name)
                                            {
                                                case "Cenoura":
                                                {
                                                    //TODO instance cenoura
                                                    //TODO TRIGGER AI 
                                                    Harvest();
                                                    
                                                }
                                                    break;
                                                case "Batata":
                                                {
                                                    //TODO TRIGGER AI 
                                                    if (Mouse.current.leftButton.wasPressedThisFrame)
                                                    {
                                                        Harvest();
                                                    }
                                                }
                                                    break;
                                                case "Alho":
                                                {
                                                    //TODO instance Alho
                                                    //TODO TriggerAI
                                                    
                                                }
                                                    break;
                                                case "Nabo":
                                                {
                                                    if (Mouse.current.leftButton.wasPressedThisFrame)
                                                    {
                                                        Harvest();
                                                    }
                                                }
                                                    break;
                                                case "Tomate":
                                                {
                                                    int times = 0;
                                                    timer = 0f;
                                                    timer += Time.deltaTime;
                                                    if (timer >= 5 && times <7)
                                                    {
                                                        Harvest();
                                                        timer = 0f;
                                                        times++;
                                                    }
                        
                                                    break;
                        
                                                }
                                            }

                    }
                }
            }
        }
    }

    void GrowToStage(int stage)
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        currentVisual = Instantiate(seedData.growthStagePrefabs[stage], transform);
        Debug.Log("done");
        
    }

    public void Harvest()
    {
        
            Instantiate(seedData.harvestItemPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
          
    }
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}