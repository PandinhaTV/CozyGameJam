using UnityEngine;
using UnityEngine.InputSystem;

public class CropAI : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D rb;

    private Animator animator;

    public bool grabbed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // atualizar animator
        animator.SetBool("Grabbed", grabbed);

        if (grabbed)
            return;

        if (Mouse.current == null) return;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // direção oposta ao mouse
        Vector2 dir = ((Vector2)transform.position - mouseWorldPos).normalized;

        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetGrabbed(bool value)
    {
        grabbed = value;
    }
}