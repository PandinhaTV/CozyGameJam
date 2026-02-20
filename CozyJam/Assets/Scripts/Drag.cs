using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    private GameObject selectedObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (selectedObject == null)
            {
                RaycastHit hit= CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Grabbable"))
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                }
                
            }
        }
        else
        {
            if (selectedObject != null)
            {
                RaycastHit hit = CastRay();

                // Verifica se largou em cima de um CropPlace
                if (hit.collider != null && hit.collider.CompareTag("CropPlace"))
                {
                    CropPlace place = hit.collider.GetComponent<CropPlace>();

                    if (place != null && !place.isOccupied)
                    {
                        place.Plant(selectedObject.GetComponent<SeedReference>().seedData);

                        Destroy(selectedObject); // remove a seed da mão
                    }
                    else
                    {
                        // Se já estiver ocupado, apenas larga no chão
                        selectedObject.GetComponent<Collider>().enabled = true;
                        Vector3 position = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.WorldToScreenPoint(selectedObject.transform.position).z );
                        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                        selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);
                        selectedObject = null;
                        selectedObject.GetComponent<Collider>().enabled = true;
                    }
                }
                else
                {
                    selectedObject.GetComponent<Collider>().enabled = true;
                    selectedObject = null;
                }
            }
            
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.WorldToScreenPoint(selectedObject.transform.position).z );
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
            selectedObject.GetComponent<Collider>().enabled = false;
        }
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
