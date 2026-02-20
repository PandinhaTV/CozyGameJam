using UnityEngine;
using System.Collections.Generic;

public class SellShop : MonoBehaviour
{


    private List<SeedReference> seedsInside = new List<SeedReference>();


    public Vector3 boxSize = new Vector3(2, 2, 2);
    public int totalMoney = 0;

    public void SellSeeds()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, boxSize / 2);

        foreach (Collider col in hits)
        {
            SeedReference seed = col.GetComponent<SeedReference>();
            if (seed != null)
            {
                GameManager.instance.playerMoney += seed.seedData.sellPrice;
                
                Destroy(seed.gameObject);
            }
        }

        Debug.Log("Money earned: " + totalMoney);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}

