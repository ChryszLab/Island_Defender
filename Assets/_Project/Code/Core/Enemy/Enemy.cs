using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float health = 100f;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        health -= 25f;
    }
}
