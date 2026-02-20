using UnityEngine;
using UnityEngine.InputSystem;

public class GrabMouse : MonoBehaviour
{
    public float grabRadius = 0.5f;
    public float grabDistance = 100f;
    public float followDistance = 5f;

    private GameObject grabbedObject;

    void Update()
    {
        // Ray sempre atualizado com a posição atual do rato
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // SphereCast SEMPRE ativo
        bool objectDetected = Physics.SphereCast(ray, grabRadius, out hit, grabDistance);

        // Quando carrega no botão esquerdo
        if (Mouse.current.leftButton.isPressed && objectDetected)
        {
            if (hit.collider.CompareTag("Grabable"))
            {
                grabbedObject = hit.collider.gameObject;

                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
            }
        }

        // Enquanto mantém pressionado
        if (Mouse.current.leftButton.isPressed && grabbedObject != null)
        {
            Vector3 targetPosition = ray.GetPoint(followDistance);
            grabbedObject.transform.position = targetPosition;
        }

        // Quando larga
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (grabbedObject != null)
            {
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = false;

                grabbedObject = null;
            }
        }
    }
    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Gizmos.color = Color.red;

        // Esfera no ponto inicial
        

        // Linha do ray
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * grabDistance);

        // Esfera no ponto final
        Gizmos.DrawWireSphere(ray.origin + ray.direction * grabDistance, grabRadius);
    }
}