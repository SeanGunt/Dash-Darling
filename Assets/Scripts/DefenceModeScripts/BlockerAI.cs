using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private float health = 100;
    [SerializeField] RectTransform healthBar;
    private void Awake()
    {
        health = 100;
        speed = 0.5f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void Move()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 50;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(50,0);
            speed = 0;

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        speed = 0.5f;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
