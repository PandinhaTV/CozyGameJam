using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance;

    private Dictionary<CropData, int> seeds = new Dictionary<CropData, int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public int GetSeedAmount(CropData crop)
    {
        if (seeds.ContainsKey(crop))
            return seeds[crop];

        return 0;
    }

    public void AddSeeds(CropData crop, int amount)
    {
        if (!seeds.ContainsKey(crop))
            seeds[crop] = 0;

        seeds[crop] += amount;
    }

    public bool UseSeed(CropData crop)
    {
        if (GetSeedAmount(crop) <= 0)
            return false;

        seeds[crop]--;
        return true;
    }
}