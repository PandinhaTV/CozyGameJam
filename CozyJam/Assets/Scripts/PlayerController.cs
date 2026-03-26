using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Normal,
    Planting,
    Shovel
}

public class PlayerController : MonoBehaviour
{
    public PlayerState currentState = PlayerState.Normal;
    public CropData selectedSeed;

    void Update()
    {
        //Debug.Log("Player state: " + currentState + " | selectedSeed: " + (selectedSeed != null ? selectedSeed.cropName : "null"));
        if (currentState != PlayerState.Normal && Mouse.current.rightButton.wasPressedThisFrame)
        {
            SetNormalState();
        }
    }

    public void SetPlantingState(CropData seed)
    {
        selectedSeed = seed;
        currentState = PlayerState.Planting;
    }

    public void SetNormalState()
    {
        selectedSeed = null;
        currentState = PlayerState.Normal;
    }

    public void SetShovelState()
    {
        selectedSeed = null;
        currentState = PlayerState.Shovel;

        Debug.Log("Shovel state activated");
    }
}