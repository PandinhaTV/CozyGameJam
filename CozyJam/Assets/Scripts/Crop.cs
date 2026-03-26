using UnityEngine;
using UnityEngine.InputSystem;

public class Crop : MonoBehaviour
{
    protected CropData data;
    public TileSlot tileSlot; // referencia para o TileSlot onde a planta está
    protected float growTimer;
    protected bool isMature;

    private SpriteRenderer sr;
    private int currentStage = -1;

    public void Initialize(CropData cropData)
    {
        data = cropData;
        growTimer = 0;
        isMature = false;

        sr = GetComponent<SpriteRenderer>();

        UpdateGrowthSprite();
    }

    void Update()
    {
        Grow();
        CheckClickForHarvest();
    }

    protected virtual void Grow()
    {
        if (isMature) return;

        growTimer += Time.deltaTime;

        UpdateGrowthSprite();
        
        if (growTimer >= data.growTime)
        {
            isMature = true;
            OnMature();
        }
    }

    protected virtual void OnMature()
    {
        Debug.Log(data.cropName + " is mature!");
        // opcional: trocar sprite para planta madura
    }

    private void CheckClickForHarvest()
    {
        if (!isMature) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit.gameObject == this.gameObject)
            {
                Harvest();
            }
        }
    }

    public virtual void Harvest()
    {
        if (!isMature) return;

        // instanciar vegetais que fogem
        for (int i = 0; i < data.yieldAmount; i++)
        {
            GameObject vegetableObj = Instantiate(data.cropPrefab, transform.position, Quaternion.identity);

            // atribuir CropData
            Crop2MUDARONOMEDISTO vegetable = vegetableObj.GetComponent<Crop2MUDARONOMEDISTO>();
            if (vegetable != null) vegetable.cropData = data;

            // definir velocidade/fuga no script da vegetal
            CropAI ai = vegetable.GetComponent<CropAI>();
            if (ai != null)
            {
                ai.SetSpeed(data.cropRunSpeed);
            }
        }

        // libera o tile
        if (tileSlot != null)
        {
            tileSlot.isOccupied = false;
            tileSlot.plantedCrop = null; // se tiver referência
        }

        Destroy(gameObject); // destrói a planta original
    }

    void UpdateGrowthSprite()
    {
        if (data.growStages == null || data.growStages.Length == 0) return;

        float growthPercent = growTimer / data.growTime;

        int stage = Mathf.FloorToInt(growthPercent * data.growStages.Length);

        stage = Mathf.Clamp(stage, 0, data.growStages.Length - 1);

        if (stage != currentStage)
        {
            currentStage = stage;
            sr.sprite = data.growStages[stage];
        }
    }
}