using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    private GameObject selectedObject;
    private Rigidbody selectedRb;

    public float throwForce = 10f;  // força do lançamento
    public float torqueForce = 5f;  // força de rotação ao lançar

    void Update()
    {
        // SEGURAR
        if (Mouse.current.leftButton.isPressed)
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null && hit.collider.CompareTag("Grabbable"))
                {
                    selectedObject = hit.collider.gameObject;
                    selectedRb = selectedObject.GetComponent<Rigidbody>();

                    // Desativa física enquanto segura
                    selectedRb.isKinematic = true;
                }
            }

            // Move o objeto junto do mouse
            if (selectedObject != null)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                Vector3 objScreenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.position);
                mousePos.z = objScreenPos.z; // mantém distância
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                selectedObject.transform.position = new Vector3(worldPos.x, 0.5f, worldPos.z);
            }
        }
        
        // SOLTAR
        else if (selectedObject != null)
        {
            RaycastHit hit = CastRay();
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
                    selectedObject = null;
                }
            }
            /*// Ativa física
            selectedRb.isKinematic = false;

            // Força de lançamento na direção da câmera
            Vector3 throwDir = Camera.main.transform.forward;
            selectedRb.AddForce(throwDir * throwForce, ForceMode.VelocityChange);

            // Adiciona torque aleatório para girar o objeto
            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * torqueForce;

            selectedRb.AddTorque(randomTorque, ForceMode.VelocityChange);*/

            // Limpar referências
            selectedObject = null;
            selectedRb = null;
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