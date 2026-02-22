using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Farming/Seed")]
public class SeedData : ScriptableObject
{
    [Header("Info")]
    public string seedName;
    public Sprite icon;
	public int quantity;

    [Header("Growth")]
    public float[] growthStageDurations; // tempo por estágio
    public GameObject[] growthStagePrefabs; // prefab por estágio

    [Header("Harvest")]
    public GameObject harvestItemPrefab;
    public int harvestAmount = 1;
    public bool regrow;
    public float regrowTime;

    [Header("Economy")]
    public int buyPrice;
    public int sellPrice;
}