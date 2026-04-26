using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float health = 100f;
    public float mouseDamage = 25f;
    public float dead_time = 0f;
    public int killCount = 10;
    public Rigidbody2D rb;
    public LevelManager levelManager;
    public bool isPlayer = false;
    public bool isHit = false;
    public Vector2 hitScale;
    Ray2D ray2D;
    Vector2 baseHitPos = Vector2.one;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        
    }
    void Update()
    {
      
        if(!isPlayer)
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        else
            rb.linearVelocity = Vector2.zero;

        if (isHit)
        {
            transform.localScale = Vector2.Lerp(baseHitPos, hitScale, 0.5f*Time.deltaTime);

        }
        else
        {
            transform.localScale = Vector2.Lerp(hitScale, baseHitPos, 0.5f*Time.deltaTime);
        }
    }
    void Dead()
    {
        if (health <= 0f)
        {
            levelManager.KillCount += killCount;
            Destroy(gameObject, dead_time);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
           isPlayer = true;
        }
        
        if(collision.tag == "Projectile")
        {
            Damage(10);
            isHit = true;
            Destroy(collision.gameObject);    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            isHit = false;
        }
    }

    private void OnMouseDown()
    {
        isHit = true;
         Damage(mouseDamage);
    }
    private void OnMouseUp()
    {
        isHit = false;
    }
    public void Damage(float damage)
    {
        health -= damage;
        Dead();
    }
}
