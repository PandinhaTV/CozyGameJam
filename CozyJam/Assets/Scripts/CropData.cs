using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Crops/Crop Data")]
public class CropData : ScriptableObject
{
    public string cropName;
    public GameObject growingPrefab;        // prefab da planta que cresce
    public GameObject cropPrefab;   // prefab do vegetal que foge quando harvest
    public float growTime = 0f;          // tempo até maturidade
    public int yieldAmount = 0;          // número de vegetais produzidos
    public int cost = 0;                // preço da semente na loja
    public int sellValue = 0;            // valor de venda por vegetal
    public float cropRunSpeed = 0f;    // velocidade de fuga do vegetal

    public Sprite[] growStages; // 3 sprites: seed -> mid -> mature
}