using UnityEngine;
using UnityEngine.InputSystem;

public class GrabDragDrop : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [Header("Grab Settings")]
    [SerializeField] private float grabRadius = 1.5f;
    [SerializeField] private LayerMask grabLayer;

    [Header("Grabed Object Info")]
    [SerializeField] private GameObject grabbedObject;
    [SerializeField] private Collider2D grabbedCollider;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || mainCamera == null)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        if (float.IsNaN(mouseScreenPos.x) || float.IsNaN(mouseScreenPos.y))
            return;

        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // AGARRAR
        if (Mouse.current.leftButton.wasPressedThisFrame && grabbedObject == null)
        {
            TryGrab(mouseWorldPos);
        }

        // ARRASTAR
        if (Mouse.current.leftButton.isPressed && grabbedObject != null)
        {
            grabbedObject.transform.position = new Vector3(
                mouseWorldPos.x,
                mouseWorldPos.y,
                grabbedObject.transform.position.z
            );
        }

        // LARGAR
        if (Mouse.current.leftButton.wasReleasedThisFrame && grabbedObject != null)
        {
            Release();
        }
    }

    void TryGrab(Vector2 mouseWorldPos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, grabRadius, grabLayer);

        if (hits.Length == 0)
            return;

        Collider2D closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(mouseWorldPos, hit.transform.position);

            if (distance < closestDistance)
            {
                closest = hit;
                closestDistance = distance;
            }
        }

        if (closest != null)
        {
            grabbedObject = closest.gameObject;
            grabbedCollider = closest.GetComponent<Collider2D>();

            if (grabbedCollider != null)
                grabbedCollider.enabled = false;
                CropAI ai = grabbedObject.GetComponent<CropAI>();
        
            if (ai != null)
                ai.SetGrabbed(true);
        }
    }

    void Release()
    {
        if (grabbedCollider != null)
            grabbedCollider.enabled = true;

        CropAI ai = grabbedObject.GetComponent<CropAI>();
        if (ai != null)
            ai.SetGrabbed(false);

        grabbedObject = null;
        grabbedCollider = null;
    }
}