using UnityEngine;

public class SellBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Crop2MUDARONOMEDISTO vegetable = other.GetComponent<Crop2MUDARONOMEDISTO>();
        if (vegetable != null && vegetable.cropData != null)
        {
            int coinsToAdd = vegetable.cropData.sellValue;

            GameManager.Instance.AddCoins(coinsToAdd);
            Destroy(other.gameObject);
        }
    }
}