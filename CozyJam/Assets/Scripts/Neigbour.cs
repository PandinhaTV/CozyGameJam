using System;
using UnityEngine;

public class Neigbour : MonoBehaviour
{
    public static Neigbour instance;

    public string[,] neighbourKeys = new string[4,6]
    {
        {"Chili","Chili","0","Garlic","0","Carrot"},
        {"0","Radish","Carrot","0","Potato","0"},
        {"0","Chili","Potato","Princess","Chili","Radish"},
        {"Tomato","Tomato","Tomato","Potato","Princess","Carrot"}
    };

    public string[,] playerKey = new string[4,6];

    public SeedData selectedSeed; // assign in inspector

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        // Inicializa playerKey
        for (int i = 0; i < playerKey.GetLength(0); i++)
        for (int j = 0; j < playerKey.GetLength(1); j++)
            playerKey[i,j] = "0";
    }

    public bool AreEqual2D(string[,] a, string[,] b)
    {
        if (a.GetLength(0) != b.GetLength(0) ||
            a.GetLength(1) != b.GetLength(1))
            return false;

        for (int i = 0; i < a.GetLength(0); i++)
        for (int j = 0; j < a.GetLength(1); j++)
            if (!string.Equals(a[i,j], b[i,j], StringComparison.OrdinalIgnoreCase))
                return false;

        return true;
    }

    void Update()
    {
        // Exemplo de adicionar valor
        playerKey[3,4] = selectedSeed.seedName;

        // Comparar
        if (AreEqual2D(playerKey, neighbourKeys))
            Debug.Log("Os arrays são exatamente iguais!");
    }
}