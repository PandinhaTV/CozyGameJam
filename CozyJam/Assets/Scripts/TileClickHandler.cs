using UnityEngine;
using UnityEngine.InputSystem;

public class TileClickHandler : MonoBehaviour
{
    public TileSlot tileSlot;

    public PlayerController playerController;

    private Collider2D tileCollider;

    private void Start()
    {
        tileSlot = GetComponent<TileSlot>();
        if (tileSlot == null)
            Debug.LogError("TileSlot not found on tile!");

        tileCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Mouse.current == null || tileSlot == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // verifica se o clique é neste tile
            /*Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                Debug.Log("Tile clicked");

                if (playerController == null) return;

                // SHOVEL
                Debug.Log("Current State: " + playerController.currentState);
                if (playerController.currentState == PlayerState.Shovel)
                {
                    tileSlot.TryRemoveCrop();

                    // voltar ao estado normal
                    playerController.SetNormalState();
    
                    return;
                }

                if (playerController.currentState == PlayerState.Planting && playerController.selectedSeed != null)
                {
                    Debug.Log("Trying to plant: " + playerController.selectedSeed.cropName);
                    tileSlot.TryPlantSeed(playerController.selectedSeed, playerController);
                }
            }
            */
            
            if (tileCollider.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Tile clicked");

                if (playerController == null) return;

                // SHOVEL
                if (playerController.currentState == PlayerState.Shovel)
                {
                    tileSlot.TryRemoveCrop();
                    playerController.SetNormalState();
                    return;
                }

                // PLANTAR
                if (playerController.currentState == PlayerState.Planting && playerController.selectedSeed != null)
                {
                    tileSlot.TryPlantSeed(playerController.selectedSeed, playerController);
                }
            }
        }
    }
}